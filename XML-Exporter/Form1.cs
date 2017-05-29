using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace XML_Exporter
{
    public partial class Form1 : Form
    {
        string treeViewSelectedNode = "";
        List<string> ListTreeParents = new List<string>();
        List<string> ListTreeNodes = new List<string>();
        List<string> ListTreeNodeValues = new List<string>();
        DataTable dt;
        List<string> ListDataTableCopyTypes = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadXmlButton_Click(object sender, EventArgs e)
        {
            string filePath = @"..\..\templates\documentation_templates\technical_specification.xml";

            //AuthorsDataSet.ReadXml(filePath);

            createDataTable(filePath);

            //dataGridView1.DataSource = AuthorsDataSet;
            //dataGridView1.DataMember = "columns";

        }

        private void createDataTable(string filePath)
        {
            dt = new DataTable("Technical_Specification");
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            foreach (XmlNode item in document["root"]["columns"])
            {
                dt.Columns.Add(new DataColumn(item["name"].InnerText));
                ListDataTableCopyTypes.Add(item["copy_type"].InnerText);
            }
            dataGridView1.DataSource = dt;
        }

        private void WriteXML_Click(object sender, EventArgs e)
        {
            writeXMLFile((DataSet)dataGridView1.DataSource);
        }

        private static void writeXMLFile(DataSet thisDataSet)
        {
            if (thisDataSet == null) { return; }

            // Create a file name to write to.
            string filename = "XmlDoc.xml";

            // Create the FileStream to write with.
            System.IO.FileStream stream = new System.IO.FileStream
                (filename, System.IO.FileMode.Create);

            // Write to the file with the WriteXml method.
            thisDataSet.WriteXml(stream);
        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                XmlNodeList nodeList = inXmlNode.ChildNodes;
                for (int i = 0; i <= nodeList.Count - 1; i++)
                {
                    XmlNode xNode = inXmlNode.ChildNodes[i];
                    string text = null;
                    if (xNode.NodeType == XmlNodeType.Element) text = xNode.Name;
                    if (xNode.NodeType == XmlNodeType.Text) text = xNode.Value;
                    inTreeNode.Nodes.Add(new TreeNode(text));
                    TreeNode tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            string FILE_NAME = textBox2.Text;
            string FILE_PATH = @"..\..\testObjects\" + FILE_NAME;
            //string FILE_PATH = @"..\..\testObjects\document.xml";

            XmlDocument document = new XmlDocument();
            document.Load(FILE_PATH);
            XmlNode node = document.DocumentElement;


            AddNode(node, treeView1.Nodes.Add(node.Name));
            treeView1.ExpandAll();

            ListTreeNodes.Clear();
            ListTreeParents.Clear();
            ListTreeNodeValues.Clear();
            returnNodesAndParents(treeView1.Nodes[0]);
        }

        private void buttonFold_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }

        private void buttonUnfold_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                treeView1.DoDragDrop(treeView1.SelectedNode, DragDropEffects.Copy);

                treeViewSelectedNode = treeView1.SelectedNode.Parent.FullPath;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the node clicked on
            treeView1.SelectedNode = this.treeView1.GetNodeAt(e.X, e.Y);
        }

        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            textBox3.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string searchNodeParent = null;
            if (dataGridView1.DataSource == null)
            {
                return;
            }
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            // If the drag operation was a copy then add the row to the other control.
            if (e.Effect == DragDropEffects.Copy)
            {
                //string cellvalue = e.Data.GetData(typeof(string)) as string;
                TreeNode CopiedTreeNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
                string cellvalue = CopiedTreeNode.Text;
                string parentnode = CopiedTreeNode.Parent.FullPath;
                var hittest = dataGridView1.HitTest(clientPoint.X, clientPoint.Y);
                if (hittest.ColumnIndex != -1)
                    dataGridView1.Columns[hittest.ColumnIndex].HeaderText = cellvalue;

                //load data from treeview
                for (int i = 0; i < ListTreeNodes.Count; i++)
                {
                    if (ListTreeNodes[i] == cellvalue && ListTreeParents[i] == parentnode)
                    {
                        if (searchNodeParent == null)
                        {
                            searchNodeParent = ListTreeParents[i];
                        }
                        int dataGridViewRowCounter = 0;
                        for (int j = ListTreeParents.IndexOf(ListTreeParents[i]); j < ListTreeParents.Count; j++)
                        {
                            if (ListTreeParents[j] == searchNodeParent && ListTreeNodes[j] == cellvalue)
                            {
                                if (dataGridView1.Rows.Count - 1 == dataGridViewRowCounter)
                                {
                                    dt.Rows.Add();
                                    //AuthorsDataSet.Tables["columns"].Rows.Add();
                                }
                                if (ListDataTableCopyTypes[hittest.ColumnIndex] == "1")
                                {
                                    for (int k = 0; k < dataGridView1.Rows.Count; k++)
                                    {
                                        dataGridView1[hittest.ColumnIndex, k].Value += ListTreeNodeValues[j] + " ";
                                    }
                                    continue;
                                }

                                dataGridView1[hittest.ColumnIndex, dataGridViewRowCounter].Value = ListTreeNodeValues[j];

                                dataGridViewRowCounter++;
                            }

                            if (j == ListTreeParents.Count - 1)
                                return;
                        }
                    }
                }
                searchNodeParent = null;
            }
        }

        private void returnNodesAndParents(TreeNode root) //za popunjavanje lista vrednosti nodova
        {
            if (root.Parent != null)
            {
                ListTreeParents.Add(root.Parent.FullPath);
                ListTreeNodes.Add(root.Text);
                if (root.FirstNode != null)
                {
                    ListTreeNodeValues.Add(root.FirstNode.Text);
                }
                else ListTreeNodeValues.Add(null);
            }

            if (root.Nodes.Count > 0)
            {
                foreach (TreeNode item in root.Nodes)
                {
                    returnNodesAndParents(item);
                }
            }
            return;
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //dataGridView1[e.ColumnIndex, e.RowIndex].Selected = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Parent == null)
            {
                textBox3.Text = null;
                return;
            }
            textBox3.Text = treeView1.SelectedNode.Parent.FullPath;
        }
    }
}

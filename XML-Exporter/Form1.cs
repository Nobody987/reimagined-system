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
        List<string> ListDataTableCopyNodesPath = new List<string>();
        List<string> ListDataTableCopyNodes = new List<string>();

        List<KeyValuePair<string, string>> ListKVPFieldsAndTables = new List<KeyValuePair<string, string>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadXmlButton_Click(object sender, EventArgs e)
        {

            //string filePath = @"..\..\templates\documentation_templates\technical_specification_test.xml";
            string filePath = textBox1.Text;
            //AuthorsDataSet.ReadXml(filePath);

            createDataTable(filePath);

            //dataGridView1.DataSource = AuthorsDataSet;
            //dataGridView1.DataMember = "columns";

        }

        private void createDataTable(string filePath)
        {
            ListDataTableCopyTypes.Clear();
            ListDataTableCopyNodes.Clear();
            ListDataTableCopyNodesPath.Clear();

            dt = new DataTable("Technical_Specification");
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            foreach (XmlNode item in document["root"]["columns"])
            {
                dt.Columns.Add(new DataColumn(item["name"].InnerText));
                ListDataTableCopyTypes.Add(item["copy_type"].InnerText);
                ListDataTableCopyNodes.Add(item["copy_node"].InnerText);
                ListDataTableCopyNodesPath.Add(item["copy_node_parent"].InnerText);
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

                //load data from treeview
                loadDataFromTree(cellvalue, parentnode, hittest.ColumnIndex);
            }
        }

        private void loadDataFromTree(string cellvalue, string parentnode, int columnIndex)
        {
            string searchNodeParent = null;

            //if (columnIndex != -1)
            //    dataGridView1.Columns[columnIndex].HeaderText = cellvalue;

            if (ListDataTableCopyTypes[columnIndex] == "1" || ListDataTableCopyTypes[columnIndex] == "2")
            {
                for (int k = 0; k < dataGridView1.Rows.Count; k++) //remove values from column
                {
                    dataGridView1[columnIndex, k].Value = "";
                }
            }
            //else if (ListDataTableCopyTypes[columnIndex] == "2")
            //{

            //    for (int k = 0; k < dataGridView1.Rows.Count; k++) //remove values from column
            //    {
            //        dataGridView1[columnIndex, k].Value = "";
            //    }
            //}

            if (ListDataTableCopyTypes[columnIndex] == "2")
            {
                loadDataFromDocInternals();
                CopyType2(6, columnIndex);
            }

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
                            if (ListDataTableCopyTypes[columnIndex] == "1")
                            {

                                for (int k = 0; k < dataGridView1.Rows.Count - 1; k++) //addding values in all rows
                                {
                                    dataGridView1[columnIndex, k].Value += ListTreeNodeValues[j] + " ";
                                }
                                continue;
                            }

                            dataGridView1[columnIndex, dataGridViewRowCounter].Value = ListTreeNodeValues[j];

                            dataGridViewRowCounter++;
                        }

                        if (j == ListTreeParents.Count - 1)
                            return;
                    }
                }
            }
            searchNodeParent = null;
        }

        private void CopyType2(int columnIndex, int copyLocationIndex)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                foreach (KeyValuePair<string, string> KVP in ListKVPFieldsAndTables)
                {
                    if (dataGridView1.Rows[i].Cells[columnIndex].Value.ToString().Contains(KVP.Key))
                    {
                        if (!dataGridView1.Rows[i].Cells[copyLocationIndex].Value.ToString().Contains(KVP.Value))
                        {
                            dataGridView1.Rows[i].Cells[copyLocationIndex].Value += KVP.Value;
                        }
                    }
                }
            }
        }

        private void loadDataFromDocInternals()
        {
            ListKVPFieldsAndTables.Clear();

            string filePathDocInternals = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\DocInternals.xml";
            //string filePathDocInternals = @"..\..\testObjects\ExcelToQV - Copy-prj\DocInternals.xml";
            //string filePathAllProperties = @"..\..\testObjects\ExcelToQV - Copy-prj\AllProperties.xml";
            string filePathAllProperties = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\AllProperties.xml";

            XmlDocument AllProperties = new XmlDocument();
            AllProperties.Load(filePathAllProperties);

            List<string> ListFieldsInAllProperties = new List<string>();
            List<string> ListTables = new List<string>();

            foreach (XmlNode node in AllProperties.SelectNodes(@"/AllProperties/FieldProperties/FieldProperties/Name"))
            {
                ListFieldsInAllProperties.Add(node.InnerText);
            }

            XmlDocument DocInternals = new XmlDocument();
            DocInternals.Load(filePathDocInternals);

            int previousNumber = 0;
            string field = "no_value";
            string table = "no_value";

            foreach (XmlNode node in DocInternals.SelectNodes(@"/DocInternals/FieldSrcTables/String"))
            {

                if (previousNumber == 0) //ako JE prva iteracija
                {
                    //previousValue = node.InnerText;
                    previousNumber = int.Parse(node.InnerText);
                    continue;
                }
                else if (previousNumber != 0) //ako NIJE prva iteracija
                {
                    int number;
                    bool resultCurrent = int.TryParse(node.InnerText, out number);

                    if (previousNumber != -1 && resultCurrent) //prethodni rezultat JE broj i trenutni rezulat JE broj
                    {
                        continue;
                    }
                    else if (previousNumber != -1 && resultCurrent == false) //prethodni rezultat JE broj i trenutni rezulat NIJE broj
                    {
                        field = ListFieldsInAllProperties[previousNumber];
                        table = node.InnerText;
                        previousNumber = -1;
                    }
                    else if (previousNumber == -1 && resultCurrent == false) //prethodni rezultat NIJE broj i trenutni rezulat NIJE broj
                    {
                        field = ListKVPFieldsAndTables[ListKVPFieldsAndTables.Count - 1].Key;
                        table = node.InnerText;
                        previousNumber = -1;
                    }
                    else if (previousNumber == -1 && resultCurrent == true) //prethodni rezultat NIJE broj i trenutni rezulat JE broj
                    {
                        previousNumber = int.Parse(node.InnerText);
                        continue;
                    }
                }
                ListKVPFieldsAndTables.Add(new KeyValuePair<string, string>(field, table));
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

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListDataTableCopyNodesPath.Count; i++)
            {
                if (ListDataTableCopyTypes[i] != "" || ListDataTableCopyTypes[i] != "2")
                {
                    loadDataFromTree(ListDataTableCopyNodes[i], ListDataTableCopyNodesPath[i], i);
                }
            }

            for (int i = 0; i < ListDataTableCopyNodesPath.Count; i++)
            {
                if (ListDataTableCopyTypes[i] == "2")
                {
                    loadDataFromTree(ListDataTableCopyNodes[i], ListDataTableCopyNodesPath[i], i);
                }
            }
        }
    }
}

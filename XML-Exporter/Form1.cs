﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace XML_Exporter
{
    public partial class Form1 : Form
    {
        string winsped_location = @"";

        string treeViewSelectedNode = "";
        List<string> ListTreeParents = new List<string>();
        List<string> ListTreeNodes = new List<string>();
        List<string> ListTreeNodeValues = new List<string>();
        System.Data.DataTable dt;

        List<string> ListDataTableCopyTypes = new List<string>();
        List<string> ListDataTableCopyNodesPath = new List<string>();
        List<string> ListDataTableCopyNodes = new List<string>();

        List<string> ListReadCSVFile = new List<string>();
        List<string> ListReadCSVColumn = new List<string>();

        List<KeyValuePair<string, string>> ListKVPFieldsAndTables = new List<KeyValuePair<string, string>>();
        private System.Data.DataTable dataTableCSV;

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

            ListReadCSVFile.Clear();
            ListReadCSVColumn.Clear();

            dt = new System.Data.DataTable("Technical_Specification");
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            foreach (XmlNode item in document["root"]["columns"])
            {
                dt.Columns.Add(new DataColumn(item["name"].InnerText));
                ListDataTableCopyTypes.Add(item["copy_type"].InnerText);
                ListDataTableCopyNodes.Add(item["copy_node"].InnerText);
                ListDataTableCopyNodesPath.Add(item["copy_node_parent"].InnerText);

                ListReadCSVFile.Add(item["read_csv_file"].InnerText);
                ListReadCSVColumn.Add(item["read_csv_column"].InnerText);
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
            string ROOT = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\80 - Application\WinSped - Transport Management - Tyres DB1 Analysis-prj\";
            string FILE_NAME = textBox2.Text;
            string FILE_PATH = ROOT + FILE_NAME;

            if (checkBox1.Checked == true)
            {
                string[] fileArray = Directory.GetFiles(ROOT, "CH*.xml", SearchOption.TopDirectoryOnly);
                foreach (string item in fileArray)
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(item);
                    XmlNode node = document.DocumentElement;

                    AddNode(node, treeView1.Nodes.Add(node.Name));
                    treeView1.ExpandAll();

                    ListTreeNodes.Clear();          //ciste se jer se popunjavaju u returnNodesAndParents
                    ListTreeParents.Clear();        //ciste se jer se popunjavaju u returnNodesAndParents
                    ListTreeNodeValues.Clear();     //ciste se jer se popunjavaju u returnNodesAndParents

                }

                foreach (TreeNode rootNode in treeView1.Nodes)
                {
                    returnNodesAndParents(rootNode);
                }
            }

            //treeView1.Nodes.Clear();

            //XmlDocument document = new XmlDocument();
            //document.Load(FILE_PATH);
            //XmlNode node = document.DocumentElement;


            //AddNode(node, treeView1.Nodes.Add(node.Name));
            //treeView1.ExpandAll();

            //ListTreeNodes.Clear();              //ciste se jer se popunjavaju u returnNodesAndParents
            //ListTreeParents.Clear();            //ciste se jer se popunjavaju u returnNodesAndParents
            //ListTreeNodeValues.Clear();         //ciste se jer se popunjavaju u returnNodesAndParents

            //foreach (TreeNode item in treeView1.Nodes)
            //{
            //    returnNodesAndParents(item);
            //}
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
            System.Drawing.Point clientPoint = dataGridView1.PointToClient(new System.Drawing.Point(e.X, e.Y));

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

            //if (ListDataTableCopyTypes[columnIndex] == "2")
            //{
            //    loadDataFromDocInternals();
            //    CopyType2(6, columnIndex);
            //    return;
            //}

            //if (ListDataTableCopyTypes[columnIndex] == "3")
            //{
            //    CopyType3(columnIndex, ListReadCSVColumn[columnIndex]);
            //    return;
            //}

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

        private void CopyType3(int columnIndex, string CSVColumnToCopy)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                foreach (DataRow item in dataTableCSV.Rows)
                {
                    if (dataGridView1.Rows[i].Cells[columnIndex].Value.ToString().Contains(item.Field<string>(0)))
                    {
                        dataGridView1.Rows[i].Cells[columnIndex].Value = item.Field<string>(CSVColumnToCopy);
                    }
                }
            }
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

            string filePathDocInternals = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\80 - Application\WinSped - Transport Management - Tyres DB1 Analysis-prj\DocInternals.xml";
            //string filePathDocInternals = @"..\..\testObjects\ExcelToQV - Copy-prj\DocInternals.xml";
            //string filePathAllProperties = @"..\..\testObjects\ExcelToQV - Copy-prj\AllProperties.xml";
            string filePathAllProperties = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\80 - Application\WinSped - Transport Management - Tyres DB1 Analysis-prj\AllProperties.xml";

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
                if (ListDataTableCopyTypes[i] != "" && ListDataTableCopyTypes[i] != "2")
                {
                    loadDataFromTree(ListDataTableCopyNodes[i], ListDataTableCopyNodesPath[i], i);
                }
            }

            for (int i = 0; i < ListDataTableCopyNodesPath.Count; i++)
            {
                if (ListDataTableCopyTypes[i] == "2")
                {
                    loadDataFromDocInternals();
                    CopyType2(6, i);
                    //loadDataFromTree(ListDataTableCopyNodes[i], ListDataTableCopyNodesPath[i], i);
                }
            }

            for (int i = 0; i < ListDataTableCopyNodesPath.Count; i++)
            {
                if (ListDataTableCopyTypes[i] == "3")
                {
                    CopyType3(i, ListReadCSVColumn[i]);
                    //loadDataFromTree(ListDataTableCopyNodes[i], ListDataTableCopyNodesPath[i], i);
                }
            }

            for (int i = 0; i < ListDataTableCopyNodesPath.Count; i++) //dodaje ID za svaki red
            {
                if (ListDataTableCopyTypes[i] == "")
                {
                    for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                    {
                        dataGridView1.Rows[j].Cells[i].Value = j;
                    }
                }
            }

            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fs = @"..\..\testObjects\WinSped - Transport Management - Tyres DB1 Analysis-prj\92 - Translation\KPI_Translation.csv";
            dataTableCSV = readCSVFile(fs);
        }

        private static System.Data.DataTable readCSVFile(string fs)
        {
            System.Data.DataTable dt = new System.Data.DataTable("CSVFile");

            using (File.OpenRead(fs))
            using (var reader = new StreamReader(fs))
            {

                bool headerRow = true;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (headerRow) //dodaje header u datatable
                    {
                        foreach (var value in values)
                        {
                            dt.Columns.Add(new DataColumn(value));
                        }
                        headerRow = false;
                        continue;
                    }
                    if (!headerRow) //dodaje ostale redove
                    {
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < values.Length; i++)
                        {
                            dr[i] = values[i];
                        }

                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
    }
}

﻿namespace XML_Exporter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loadXMLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ReadXmlButton = new System.Windows.Forms.Button();
            this.AuthorsDataSet = new System.Data.DataSet();
            this.WriteXML = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonUnfold = new System.Windows.Forms.Button();
            this.buttonFold = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadXMLBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AuthorsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(819, 289);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            // 
            // ReadXmlButton
            // 
            this.ReadXmlButton.Location = new System.Drawing.Point(12, 333);
            this.ReadXmlButton.Name = "ReadXmlButton";
            this.ReadXmlButton.Size = new System.Drawing.Size(84, 23);
            this.ReadXmlButton.TabIndex = 2;
            this.ReadXmlButton.Text = "Read XML";
            this.ReadXmlButton.UseVisualStyleBackColor = true;
            this.ReadXmlButton.Click += new System.EventHandler(this.ReadXmlButton_Click);
            // 
            // AuthorsDataSet
            // 
            this.AuthorsDataSet.DataSetName = "NewDataSet";
            // 
            // WriteXML
            // 
            this.WriteXML.Location = new System.Drawing.Point(736, 333);
            this.WriteXML.Name = "WriteXML";
            this.WriteXML.Size = new System.Drawing.Size(95, 23);
            this.WriteXML.TabIndex = 4;
            this.WriteXML.Text = "Write XML";
            this.WriteXML.UseVisualStyleBackColor = true;
            this.WriteXML.Click += new System.EventHandler(this.WriteXML_Click);
            // 
            // treeView1
            // 
            this.treeView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeView1.Location = new System.Drawing.Point(837, 38);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(431, 289);
            this.treeView1.TabIndex = 5;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(837, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Read XML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonUnfold
            // 
            this.buttonUnfold.Location = new System.Drawing.Point(1184, 362);
            this.buttonUnfold.Name = "buttonUnfold";
            this.buttonUnfold.Size = new System.Drawing.Size(84, 23);
            this.buttonUnfold.TabIndex = 7;
            this.buttonUnfold.Text = "Unfold";
            this.buttonUnfold.UseVisualStyleBackColor = true;
            this.buttonUnfold.Click += new System.EventHandler(this.buttonUnfold_Click);
            // 
            // buttonFold
            // 
            this.buttonFold.Location = new System.Drawing.Point(1184, 333);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(84, 23);
            this.buttonFold.TabIndex = 8;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.buttonFold_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(837, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(431, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "CH28.xml";
            // 
            // textBox3
            // 
            this.textBox3.AllowDrop = true;
            this.textBox3.Location = new System.Drawing.Point(837, 405);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(431, 20);
            this.textBox3.TabIndex = 10;
            this.textBox3.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox3_DragDrop);
            this.textBox3.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox3_DragEnter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(819, 20);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "..\\..\\templates\\documentation_templates\\technical_specification.xml";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(102, 333);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Copy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Read XLS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 552);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Open QVW";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox4
            // 
            this.textBox4.AllowDrop = true;
            this.textBox4.Location = new System.Drawing.Point(113, 555);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(431, 20);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "C:\\Users\\slobodan.sredojevic\\Desktop\\ExcelToQV\\ExcelToQV.qvw";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(736, 362);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Write Excel";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1274, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "All Objects?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 587);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.buttonFold);
            this.Controls.Add(this.buttonUnfold);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.WriteXML);
            this.Controls.Add(this.ReadXmlButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.loadXMLBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AuthorsDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource loadXMLBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ReadXmlButton;
        private System.Data.DataSet AuthorsDataSet;
        private System.Windows.Forms.Button WriteXML;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonUnfold;
        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}


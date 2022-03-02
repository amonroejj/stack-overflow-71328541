using System;
using System.ComponentModel;
using System.Windows.Forms;




namespace DataBindingTest
{
    public class Form1 : Form
    {
        private readonly BindingSource  _bs = new BindingSource() { DataSource = typeof(Outer) };
        private Outer _dummy;
        
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1        .DataBindings.Add(new Binding("Text"        , _bs, "Comment"                    , false, DataSourceUpdateMode.OnPropertyChanged));
            dataGridView1   .DataBindings.Add(new Binding("DataSource"  , _bs, "Innards"                    , false, DataSourceUpdateMode.Never));
            button1         .DataBindings.Add(new Binding("Enabled"     , _bs, "Dirty"                      , false, DataSourceUpdateMode.Never));
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            _dummy = new Outer("hello world");
            _bs.DataSource = _dummy;
        }
















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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 220);
            this.dataGridView1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
    }













    public class Outer
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool        _dirty;
        private string      _comment;

        public Outer(string comment)
        {
            _comment = comment;
            for (int i = 0; i < 5; i++)
            {
                Inner inner = new Inner();
                inner.PropertyChanged += ChildPropertyChanged;
                Innards.Add(inner);
            }
        }

        private void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dirty = true;
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if(value != _comment)
                {
                    _comment = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Comment"));
                    Dirty = true;
                }
            }
        }
            
        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                if(value != _dirty)
                {
                    _dirty = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dirty"));
                    System.Diagnostics.Debug.WriteLine($"outer -- Dirty: {_dirty}");
                    System.Diagnostics.Debug.WriteLine($"outer PropertyChanged: {PropertyChanged}");
                }
            }
        }

        public BindingList<Inner>   Innards    { get; } = new BindingList<Inner>();
    }



    public class Inner
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool                    _dirty;
        private bool                    _isExcluded;

        public bool IsExcluded
        {
            get { return _isExcluded; }
            set
            {
                if(value != _isExcluded)
                {
                    _isExcluded = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsExcluded"));
                    Dirty = true;
                }
            }
        }            
            
        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                if(value != _dirty)
                {
                    _dirty = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dirty"));
                    System.Diagnostics.Debug.WriteLine($"inner -- Dirty: {_dirty}");
                    System.Diagnostics.Debug.WriteLine($"inner PropertyChanged: {PropertyChanged}");
                }
            }
        }
    }


}

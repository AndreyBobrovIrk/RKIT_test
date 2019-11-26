using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitDataBase();
        }

        static readonly string db_path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Data", "db.sqlite");
        SQLiteConnection db_connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", db_path));

        void ShowError(String text)
        {
            System.Windows.Forms.MessageBox.Show(this, text, "Error", MessageBoxButtons.OK);
        }

        bool ExecuteCommand(SQLiteCommand a_command)
        {
            try
            {
                db_connection.Open();
                a_command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ShowError(err.Message);
                return false;
            }
            finally
            {
                db_connection.Close();
            }

            return true;
        }

        void InitDataBase()
        {
            if (System.IO.File.Exists(db_path))
            {
                return;
            }

            ExecuteCommand(
                new SQLiteCommand("create table table1 (field1 BLOB);", db_connection)
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK ||
                !System.IO.File.Exists(openFileDialog1.FileName)
            )
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            System.IO.TextWriter textWriter = new System.IO.StringWriter(sb);

            XslCompiledTransform myXslTransform;
            XsltArgumentList args = new XsltArgumentList();

            myXslTransform = new XslCompiledTransform();

            try
            {
                myXslTransform.Load(
                    String.Concat(System.Environment.CurrentDirectory, @"\Data\data.xslt")
                );

                myXslTransform.Transform(openFileDialog1.FileName, args, textWriter);
                richTextBox1.Text = textWriter.ToString();

                SQLiteCommand command = new SQLiteCommand("insert into table1 (field1) values (@value)", db_connection);

                command.Parameters.Add("@value", DbType.String);
                command.Parameters["@value"].Value = System.IO.File.ReadAllText(openFileDialog1.FileName);

                ExecuteCommand(command);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(this, err.Message, "Error", MessageBoxButtons.OK);
                richTextBox1.Text = String.Empty;
                ShowError(err.Message);
            }
            finally
            {
                openFileDialog1.FileName = String.Empty;
            }
        }
    }
}

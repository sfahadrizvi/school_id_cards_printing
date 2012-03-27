using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;        // Database

using System.Globalization;             // Date


using System.Collections;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;

using Microsoft.Win32;

namespace CardPrint
{
    public partial class Form1 : Form
    {
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public void SaveJPG100Image(Bitmap bmp, string filename)
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            bmp.Save(filename, GetEncoder(ImageFormat.Jpeg), encoderParameters);
        }

        public ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void DBBrowseButton_Click(object sender, EventArgs e)
        {
            if (DBSelect.ShowDialog() == DialogResult.OK)
            {
                this.DBName.Text = DBSelect.FileName;
            }

        }

        private void faceImageFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath == thumbImageFolder.Text)
                {
                    MessageBox.Show("Face Image Folder Can Not Be The Same As Thumb Image Folder");
                    return;
                }
                if (folderBrowserDialog1.SelectedPath == destinationFolder.Text)
                {
                    MessageBox.Show("Face Image Folder Can Not Be The Same As Destination Image Folder");
                    return;
                }
                this.faceImageFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void thumbImageFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath == faceImageFolder.Text)
                {
                    MessageBox.Show("Thumb Image Folder Can Not Be The Same As Face Image Folder");
                    return;
                }
                if (folderBrowserDialog1.SelectedPath == destinationFolder.Text)
                {
                    MessageBox.Show("Thumb Image Folder Can Not Be The Same As Destination Image Folder");
                    return;
                }
                
                this.thumbImageFolder.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void destinationFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath == faceImageFolder.Text)
                {
                    MessageBox.Show("Destination Image Folder Can Not Be The Same As Face Image Folder");
                    return;
                }
                
                if (folderBrowserDialog1.SelectedPath == thumbImageFolder.Text)
                {
                    MessageBox.Show("Destination Image Folder Can Not Be The Same As Thumb Image Folder");
                    return;
                }

                this.destinationFolder.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void readDBButton_Click(object sender, EventArgs e)
        {
            string appfolder = Path.GetDirectoryName(Application.ExecutablePath);
            const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "software\\Card_Printing";
            const string keyName = userRoot + "\\" + subkey;
            try
            {
                if ((int)Registry.GetValue(keyName, "Cards_v3", -1) == -1)
                {
                    if (DateTime.Now.Year == 2011 && DateTime.Now.Month==6)
                    {
                        Registry.SetValue(keyName, "Cards_v3", 0, RegistryValueKind.DWord);
                        File.WriteAllText( appfolder +"\\importantDataV3", getMd5Hash("0"));
                    }
                }


                int tInteger = (int)Registry.GetValue(keyName, "Cards_v3", -1);
                string tIntegerMd5 = File.ReadAllText(appfolder + "\\importantDataV3");
                if (tIntegerMd5 != getMd5Hash(tInteger.ToString()))
                {
                    MessageBox.Show("Cards Number Not Matching");
                    return;
                }

                if (tInteger > 1200)
                {
                     MessageBox.Show("Max Cards Done");
                     return;
                }

                string templateTxt = File.ReadAllText(appfolder +"\\Templates.mdb");
                string templateMd5 = getMd5Hash(templateTxt);
                if (templateMd5 != "b181bc3fb7e07ffc7fbf6d99dab706bf")
                {               //    b181bc3fb7e07ffc7fbf6d99dab706bf
                    MessageBox.Show("template Modified");
                    return;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show (ee.Message);
                return;
            }
            
            if (this.DBName.Text.Length > 0)
            {
                try
                {
                    string DB_CONN_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.DBName.Text;
                    OleDbConnection conn = new OleDbConnection(DB_CONN_STRING);
                    conn.Open();
                    OleDbDataReader dr;
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Personal_Info order by(ID_Person)", conn);
                    dr = cmd.ExecuteReader();

                    //create sub folders
                    Directory.CreateDirectory(destinationFolder.Text + "\\A_B");
                    //Directory.CreateDirectory(destinationFolder.Text + "\\B");
                    Directory.CreateDirectory(destinationFolder.Text + "\\C");
                    Directory.CreateDirectory(destinationFolder.Text + "\\Teacher");


                    while(dr.Read())
                    {
                        System.Drawing.Bitmap bitmap = null;
                          string file_name;
                          int tInteger2 = (int)Registry.GetValue(keyName, "Cards_v3", -1);
                            ++tInteger2;
                            File.WriteAllText(appfolder + "importantDataV3", getMd5Hash(tInteger2.ToString()));
                          Registry.SetValue(keyName, "Cards_v3", tInteger2, RegistryValueKind.DWord);


                        if ((dr.IsDBNull(dr.GetOrdinal("FirstName"))))
                        {                            
                            continue;
                        }
                              string DB_CONN_STRING1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\Card_Printer\\Templates.mdb";
                            OleDbConnection conn1 = new OleDbConnection(DB_CONN_STRING1);
                            conn1.Open();
                            OleDbDataReader dr1;
                            OleDbCommand cmd1;
                        
                            if (!dr.IsDBNull(dr.GetOrdinal("PackageChoice")) && dr["PackageChoice"].ToString() == "1")
                            {
                                Assembly a = Assembly.GetExecutingAssembly();
                                //string[] resNames = a.GetManifestResourceNames();
                                file_name = destinationFolder.Text +"\\A_B\\" + dr["firstname"].ToString();

                                if (!dr.IsDBNull(dr.GetOrdinal("lastname")))
                                    file_name = file_name + "_" + dr["lastname"].ToString();

                                file_name = file_name + "_" + dr["ID_Person"].ToString() + ".jpg";

                                bitmap = new System.Drawing.Bitmap(a.GetManifestResourceStream("CardPrint.packageA.JPG"));
                                cmd1 = new OleDbCommand("SELECT * FROM PackageA", conn1);
                                dr1 = cmd1.ExecuteReader();

                            }
                            else
                                if (!dr.IsDBNull(dr.GetOrdinal("PackageChoice")) && dr["PackageChoice"].ToString() == "2")
                                {
                                    Assembly a = Assembly.GetExecutingAssembly();
                                    //string[] resNames = a.GetManifestResourceNames();
                                    file_name = destinationFolder.Text + "\\A_B\\" + dr["firstname"].ToString();

                                    if (!dr.IsDBNull(dr.GetOrdinal("lastname")))
                                        file_name = file_name + "_" + dr["lastname"].ToString();

                                    file_name = file_name + "_" + dr["ID_Person"].ToString() + ".jpg";

                                
                                    bitmap = new System.Drawing.Bitmap(a.GetManifestResourceStream("CardPrint.packageB.JPG"));
                                    cmd1 = new OleDbCommand("SELECT * FROM PackageB", conn1);
                                    dr1 = cmd1.ExecuteReader();
                                }
                                else
                                    if (!dr.IsDBNull(dr.GetOrdinal("PackageChoice")) && (dr["PackageChoice"].ToString() == "6" ||dr["PackageChoice"].ToString() == "-1") )
                                    {
                                        Assembly a = Assembly.GetExecutingAssembly();
                                        //string[] resNames = a.GetManifestResourceNames();
                                        file_name = destinationFolder.Text + "\\Teacher\\" + dr["firstname"].ToString();

                                        if (!dr.IsDBNull(dr.GetOrdinal("lastname")))
                                            file_name = file_name + "_" + dr["lastname"].ToString();

                                        file_name = file_name + "_" + dr["ID_Person"].ToString() + ".jpg";

                                
                                        bitmap = new System.Drawing.Bitmap("c:\\Card_Printer\\packageTeacher.jpg");
                                        cmd1 = new OleDbCommand("SELECT * FROM PackageTeacher", conn1);
                                        dr1 = cmd1.ExecuteReader();
                                    }
                                    else
                                        if (!dr.IsDBNull(dr.GetOrdinal("PackageChoice")) && dr["PackageChoice"].ToString() == "3")
                                        {
                                            Assembly a = Assembly.GetExecutingAssembly();
                                            //string[] resNames = a.GetManifestResourceNames();
                                            file_name = destinationFolder.Text + "\\C\\" + dr["firstname"].ToString();

                                            if (!dr.IsDBNull(dr.GetOrdinal("lastname")))
                                                file_name = file_name + "_" + dr["lastname"].ToString();

                                            file_name = file_name + "_" + dr["ID_Person"].ToString() + ".jpg";


                                            bitmap = new System.Drawing.Bitmap(a.GetManifestResourceStream("CardPrint.packageC.JPG"));
                                            cmd1 = new OleDbCommand("SELECT * FROM PackageC", conn1);
                                            dr1 = cmd1.ExecuteReader();
                                        }
                                else
                                {
                                    //MessageBox.Show("Incorrect Package");

                                    continue;
                                }

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                g.CompositingQuality = CompositingQuality.HighQuality;

                                {
                                   

                                    while(dr1.Read())
                                    {
                                       
                                        if (dr1["Type"].ToString() == "1")
                                        {
                                            string Text=string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"],(int)dr1["cg"] ,(int)dr1["cb"] ));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString() );
                                            float sz =  (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs= FontStyle.Regular;
                                            
                                            if (dr1["isbold"].ToString() == "1")
                                               fs = fs | FontStyle.Bold;
                                        
                                            Font font = new Font(family, sz,fs   ) ;
                                            StringFormat sf = new StringFormat();
                                            
                                            //sf.FormatFlags = StringFormatFlags. .DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            { 
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                            if (dr1["align"].ToString() == "left")
                                            {
                                                 sf.Alignment = StringAlignment.Near;
                                            }
                                            else
                                            {
                                              sf.Alignment = StringAlignment.Far;
                                            }

                                      
                                            if (dr1["fromfield1"].ToString() != "-1")
                                                Text = dr[(int)dr1["fromfield1"]].ToString();
                                            if (dr1["fromfield2"].ToString() != "-1")
                                                Text = Text + " " + dr[(int)dr1["fromfield2"]].ToString();
                                            if (dr1["fromfield3"].ToString() != "-1")
                                                Text = Text + " " + dr[(int)dr1["fromfield3"]].ToString();

                                            //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            g.DrawString(Text, font, solidBrush, new RectangleF( (float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);
                                            
                                        }


                                        if (dr1["Type"].ToString() == "2")//DOB
                                        {

                                            //    MessageBox.Show(dr1["fontname"].ToString() +" "+dr1["fontsize"].ToString());
                                            string Text = string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"], (int)dr1["cg"], (int)dr1["cb"]));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString());
                                            float sz = (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs = FontStyle.Regular;

                                            if (dr1["isbold"].ToString() == "1")
                                                fs = fs | FontStyle.Bold;



                                            Font font = new Font(family, sz, fs);
                                            StringFormat sf = new StringFormat();
                                           // sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            {
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                            if (dr1["align"].ToString() == "left")
                                            {
                                                sf.Alignment = StringAlignment.Near;
                                            }
                                            else
                                            {
                                                sf.Alignment = StringAlignment.Far;
                                            }

                                            if (dr1["fromfield1"].ToString() != "-1")
                                                Text = dr[(int)dr1["fromfield1"]].ToString();
                                            Text= Text.Replace("/", ".");
                                            //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            g.DrawString(Text, font, solidBrush, new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);
                                           
                                        }

                                        if (dr1["Type"].ToString() == "3")//Face Image
                                        {
                                            string image_source;
                                            image_source = faceImageFolder.Text;
                                            if (dr1["fromfield1"].ToString() != "-1")
                                                image_source = image_source + "\\" + dr[(int)dr1["fromfield1"]].ToString();
                                            else
                                                break;
                                            //System.Drawing.Bitmap face_image = new Bitmap(image_source);

                                            g.DrawImage(System.Drawing.Image.FromFile(image_source), new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())));
                                           // face_image.Dispose();

                                        }

                                        if (dr1["Type"].ToString() == "4")//Thumb Image
                                        {
                                            string image_source;
                                            image_source = thumbImageFolder.Text;
                                            if (dr1["fromfield1"].ToString() != "-1")
                                                image_source = image_source + "\\" + dr[(int)dr1["fromfield1"]].ToString();
                                            else
                                                break;
                                            g.DrawImage(System.Drawing.Image.FromFile(image_source), new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())));
                                         
                                            
                                        }
                                        if (dr1["Type"].ToString() == "5")//city, state zip
                                        {

                                           string Text = string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"], (int)dr1["cg"], (int)dr1["cb"]));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString());
                                            float sz = (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs = FontStyle.Regular;

                                            if (dr1["isbold"].ToString() == "1")
                                                fs = fs | FontStyle.Bold;



                                            Font font = new Font(family, sz, fs);
                                            StringFormat sf = new StringFormat();
                                            // sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            {
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                                if (dr1["align"].ToString() == "left")
                                                {
                                                    sf.Alignment = StringAlignment.Near;
                                                }
                                                else
                                                {
                                                    sf.Alignment = StringAlignment.Far;
                                                }

                                            if (dr1["fromfield1"].ToString() != "-1")
                                                Text = dr[(int)dr1["fromfield1"]].ToString();

                                            if (dr1["fromfield2"].ToString() != "-1")
                                                Text = Text + ", " + dr[(int)dr1["fromfield2"]].ToString();

                                            if (dr1["fromfield3"].ToString() != "-1")
                                                Text = Text + " " + dr[(int)dr1["fromfield3"]].ToString();

                                            //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            g.DrawString(Text, font, solidBrush, new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);

                                        }

                                        if (dr1["Type"].ToString() == "6")//ID with five digits
                                        {
                                            string Text = string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"], (int)dr1["cg"], (int)dr1["cb"]));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString());
                                            float sz = (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs = FontStyle.Regular;

                                            if (dr1["isbold"].ToString() == "1")
                                                fs = fs | FontStyle.Bold;

                                            Font font = new Font(family, sz, fs);
                                            StringFormat sf = new StringFormat();

                                            //sf.FormatFlags = StringFormatFlags. .DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            {
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                                if (dr1["align"].ToString() == "left")
                                                {
                                                    sf.Alignment = StringAlignment.Near;
                                                }
                                                else
                                                {
                                                    sf.Alignment = StringAlignment.Far;
                                                }


                                            if (dr1["fromfield1"].ToString() != "-1")
                                                Text = dr[(int)dr1["fromfield1"]].ToString();
                                            if (dr1["fromfield2"].ToString() != "-1")
                                                Text = Text + " " + dr[(int)dr1["fromfield2"]].ToString();
                                            if (dr1["fromfield3"].ToString() != "-1")
                                                Text = Text + " " + dr[(int)dr1["fromfield3"]].ToString();

                                            //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            g.DrawString(string.Format("{0:00000}",Convert.ToInt32(Text)), font, solidBrush, new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);

                                        }

                                        if (dr1["Type"].ToString() == "7")//Constant
                                        {
                                            string Text = string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"], (int)dr1["cg"], (int)dr1["cb"]));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString());
                                            float sz = (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs = FontStyle.Regular;

                                            if (dr1["isbold"].ToString() == "1")
                                                fs = fs | FontStyle.Bold;

                                            Font font = new Font(family, sz, fs);
                                            StringFormat sf = new StringFormat();

                                            //sf.FormatFlags = StringFormatFlags. .DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            {
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                                if (dr1["align"].ToString() == "left")
                                                {
                                                    sf.Alignment = StringAlignment.Near;
                                                }
                                                else
                                                {
                                                    sf.Alignment = StringAlignment.Far;
                                                }


                                            if (dr1["comment"].ToString() != "-1")
                                                Text = dr1["comment"].ToString();
                                            
                                            //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            g.DrawString(Text, font, solidBrush, new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);

                                        }

                                        if (dr1["Type"].ToString() == "8")
                                        {
                                            string Text = string.Empty;
                                            Brush solidBrush = new SolidBrush(Color.FromArgb((int)dr1["cr"], (int)dr1["cg"], (int)dr1["cb"]));
                                            FontFamily family = new FontFamily(dr1["fontname"].ToString());
                                            float sz = (float)Convert.ToDouble(dr1["fontsize"].ToString());

                                            FontStyle fs = FontStyle.Regular;

                                            if (dr1["isbold"].ToString() == "1")
                                                fs = fs | FontStyle.Bold;

                                            Font font = new Font(family, sz, fs);
                                            StringFormat sf = new StringFormat();

                                            //sf.FormatFlags = StringFormatFlags. .DirectionRightToLeft;
                                            if (dr1["align"].ToString() == "center")
                                            {
                                                sf.Alignment = StringAlignment.Center;
                                            }
                                            else
                                                if (dr1["align"].ToString() == "left")
                                                {
                                                    sf.Alignment = StringAlignment.Near;
                                                }
                                                else
                                                {
                                                    sf.Alignment = StringAlignment.Far;
                                                }

                                            
                                            if (dr1["fromfield1"].ToString() != "-1")
                                                Text = dr[(int)dr1["fromfield1"]].ToString();
                                            if (Text.Length <= 0)
                                            {

                                                OleDbCommand cmd8 = new OleDbCommand("SELECT * FROM Customers", conn);
                                                OleDbDataReader dr8;
                                                dr8 = cmd8.ExecuteReader();
                                                dr8.Read();
                                                if (!dr8.IsDBNull(dr8.GetOrdinal("SchoolName")))
                                                    Text = dr8["SchoolName"].ToString();
                                                dr8.Close();
                                                dr8.Dispose();
                                                //Text = Text + " " + dr1["x"].ToString() + " " + dr1["y"].ToString() + " " + dr1["w"].ToString() + " " + dr1["h"].ToString();
                                            }
                                             g.DrawString(Text, font, solidBrush, new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())), sf);


                                       
                                                                                
                                        }


                                        if (dr1["Type"].ToString() == "9")//School Monogram Image
                                        {

                                            if (SchoolMonogramImageTextBox.Text.Length > 0)
                                            {
                                                string image_source;
                                                image_source = SchoolMonogramImageTextBox.Text;
                                               
                                               
                                                g.DrawImage(System.Drawing.Image.FromFile(image_source), new RectangleF((float)Convert.ToDouble(dr1["x"].ToString()), (float)Convert.ToDouble(dr1["y"].ToString()), (float)Convert.ToDouble(dr1["w"].ToString()), (float)Convert.ToDouble(dr1["h"].ToString())));
                                                // face_image.Dispose();

                                            }
                                        }

                                 }

                                    dr1.Close();
                                    dr1.Dispose();
                                    conn1.Close();
                                    conn1.Dispose();
                                }
                           }


                           if (bitmap != null)
                            {
                                bitmap.Save(file_name, System.Drawing.Imaging.ImageFormat.Jpeg);
                                //  bitmap.Save( "c:\\123.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                //SaveJPG100Image(bitmap, "c:\\abc.jpg");
                                bitmap.Dispose();
                            }

                           System.Threading.Thread.Sleep(500);
                    }
                        
                        // get a reference to the current assembly
           
                    dr.Close();
                    conn.Close();
                }
                catch (OleDbException ex)
                {
                    string error_msg;
                    error_msg = "Read Error : " + ex.Message ; 
                }
                finally
                {
                    MessageBox.Show("Conversion Completed");
                }

            }

        }

        private void faceImageFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void DBName_TextChanged(object sender, EventArgs e)
        {

            this.faceImageFolder.Text = Path.GetDirectoryName(this.DBName.Text) + "\\Photo";
            this.thumbImageFolder.Text = Path.GetDirectoryName(this.DBName.Text) + "\\FingerPr";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.SchoolMonogramImageTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void DBSelect_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Globalization;
using Microsoft.Win32;
using Spire.Xls;
using Spire.PdfViewer.Forms;
using System.IO;
using Dragablz;
using System.Collections;
using System.Threading;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;

namespace PrintexV
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // variables 
        public static int i = 0;
        private static string username = "";

        public MainWindow()
        {
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex"));
                TextCreate(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/modpas.txt"));
                TextCreate(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/Users.txt"));
                new Account().Show();

                InitializeComponent();
            }
            else
            {
                InitializeComponent();
            }

        }

        public void AutoForm()
        {

        }

        public void affectUserName(string tera)
        {
            WriteUserName(tera);
        }

        // create a text file 
        // vars belongs to creation of the text file
        private static ReaderWriterLockSlim reda = new ReaderWriterLockSlim();
        // the responsible method
        private void TextCreate(string x)
        {
            reda.EnterWriteLock();
            try
            {
                File.Create(x);

            }
            finally
            {
                reda.ExitWriteLock();
            }
        }

        // create an excel File

        public void CreateExcels()
        {
            // Create Excel workbook and the sheets
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Name = "Mois";
            Worksheet sheer = workbook.Worksheets[1];
            sheer.Name = "Annee";

            // set the ranges values

            // annee sheet
            sheer.Range["B3"].Value = "#";
            sheer.Range["C3"].Value = "Nom";
            sheer.Range["D3"].Value = "Prenom";
            sheer.Range["E3"].Value = "Niveau";
            sheer.Range["F3"].Value = "Paiement";
            sheer.Range["G3"].Value = "Date";
            sheer.Range["H3"].Value = "Auteur";

            // mois sheet
            sheet.Range["B3"].Value = "#";
            sheet.Range["C3"].Value = "Nom";
            sheet.Range["D3"].Value = "Prenom";
            sheet.Range["E3"].Value = "Niveau";
            sheet.Range["F3"].Value = "Matiere";
            sheet.Range["G3"].Value = "Prof";
            sheet.Range["H3"].Value = "Groupe";
            sheet.Range["I3"].Value = "Paiement";
            sheet.Range["J3"].Value = "Date";
            sheet.Range["K3"].Value = "Auteur";

            // styles
            sheet.Range["B3:K3"].Style.Color = System.Drawing.Color.Teal;
            sheer.Range["B3:H3"].Style.Color = System.Drawing.Color.Teal;
            sheet.Range["B3:K3"].Style.Font.Color = System.Drawing.Color.White;
            sheer.Range["B3:H3"].Style.Font.Color = System.Drawing.Color.White;
            sheet.Range["C3:J3"].ColumnWidth = 18;
            sheet.Range["B3"].ColumnWidth = 6;
            sheet.Range["K3"].ColumnWidth = 24;
            sheet.Range["B3:K3"].RowHeight = 24;
            sheer.Range["C3:G3"].ColumnWidth = 18;
            sheer.Range["H3"].ColumnWidth = 24;
            sheer.Range["B3"].ColumnWidth = 8;
            sheer.Range["B3:H3"].RowHeight = 24;

            // style font - The header
            sheet.Range["B3:K830"].Style.Font.FontName = "Verdana";
            sheet.Range["B3:K830"].Style.Font.Size = 12;
            sheet.Range["B3:K830"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["B3:K830"].Style.VerticalAlignment = VerticalAlignType.Center;

            // Style the Font - The header
            sheer.Range["B3:H830"].Style.Font.FontName = "Verdana";
            sheer.Range["B3:H830"].Style.Font.Size = 12;
            sheer.Range["B3:H830"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheer.Range["B3:H830"].Style.VerticalAlignment = VerticalAlignType.Center;

            //ExcelColors.LightTurquoise;

            // Style the borders
            sheet.Range["B3:K830"].BorderInside(LineStyleType.Thin, ExcelColors.Teal);
            sheet.Range["B3:K830"].BorderAround(LineStyleType.Medium, ExcelColors.Teal);

            sheer.Range["B3:H830"].BorderInside(LineStyleType.Thin, ExcelColors.Teal);
            sheer.Range["B3:H830"].BorderAround(LineStyleType.Medium, ExcelColors.Teal);

            // Style the sheets

            // Save the Excel File
            // Process open file dialog box results
            WriteName("Bridges.xlsx");
            workbook.SaveToFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/Bridges.xlsx"), ExcelVersion.Version2013);
        }

        // Create pdf file
        //private static int x = 0;
        public void CreatePdf(string nom, string prenom, string prof, string niveau, string matiere, string groupe, string prix)
        {
            // Create a new PDF document

            PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();

            document.Info.Title = "Created with PDFsharp";
            // Create an empty page
            PdfSharp.Pdf.PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            page.Size = PageSize.A5;
            page.Orientation = PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont fonti = new XFont("Verdana", 20, XFontStyle.Regular);
            XFont font = new XFont("Verdana", 16, XFontStyle.Regular);
            XFont fonta = new XFont("Verdana", 12, XFontStyle.Regular);

            // create an image
            XImage img = XImage.FromFile(@"Bridges.PNG");
            const double tx = 190, ty = 100, txa = 1010;
            double wid = img.PixelWidth * 18 / img.HorizontalResolution;
            double hei = img.PixelHeight * 18 / img.HorizontalResolution;

            gfx.DrawImage(img, (tx - wid) / 2, (ty - hei) / 2, wid, hei);
            gfx.DrawImage(img, (txa - wid) / 2, (ty - hei) / 2, wid, hei);

            // Draw the text
            gfx.DrawString("Bridges Of Knowledge Center", font, XBrushes.Black, new XRect(0, -160, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Nom: " + nom, font, XBrushes.Black, new XRect(-(180 - nom.Length * 4), -80, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Prenom: " + prenom, font, XBrushes.Black, new XRect(-(168 - prenom.Length * 4), -50, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Prof: " + prof, font, XBrushes.Black, new XRect(-(184 - prof.Length * 4), -20, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Matiere: " + matiere, font, XBrushes.Black, new XRect(-(170 - matiere.Length * 4), 10, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Niveau: " + niveau, font, XBrushes.Black, new XRect(-(172 - niveau.Length * 4), 40, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Groupe: " + groupe, font, XBrushes.Black, new XRect(90 - groupe.Length * 4, 40, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Paiement: " + prix + " DZD", font, XBrushes.Black, new XRect(-(162 - prix.Length * 4), 70, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Le : " + DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("fr-FR")), fonta, XBrushes.Black, new XRect(180, 120, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("à : " + DateTime.Now.ToString("hh:mm tt"), fonta, XBrushes.Black, new XRect(174, 140, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("© " + DateTime.Now.Year + ".", fonta, XBrushes.Black, new XRect(0, 200, page.Width, page.Height), XStringFormats.Center);
            XPen pen = new XPen(XColors.Navy, .5);
            gfx.DrawLine(pen, 70, 365, 520, 365);
            gfx.DrawString("Cité Hchachna en face CEM 1272 - Batna.", fonta, XBrushes.Black, new XRect(-135, 165, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Fixe : 033 28 30 67. Mobile : 0655 16 09 34", fonta, XBrushes.Black, new XRect(135, 165, page.Width, page.Height), XStringFormats.Center);

            XImage image = XImage.FromFile(@"face.png");
            XImage imagea = XImage.FromFile(@"twitter.png");
            XImage imageb = XImage.FromFile(@"insta.png");
            XImage imagec = XImage.FromFile(@"google.png");
            XImage imaged = XImage.FromFile(@"link.png");
            const double dx = 500, dy = 790, dxa = 550, dxb = 600, dxc = 650, dxd = 700;

            double width = image.PixelWidth * 4.5 / image.HorizontalResolution;

            double height = image.PixelHeight * 4.5 / image.HorizontalResolution;

            gfx.DrawImage(image, (dx - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imagea, (dxa - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imageb, (dxb - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imagec, (dxc - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imaged, (dxd - width) / 2, (dy - height) / 2, width, height);

            // Save the document...
            fillExcelFile(nom, prenom, prof, niveau, matiere, groupe, prix, username);
            
            // Process open file dialog box results
            string mira = i + "spec.pdf";
            string petha = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            document.Save(System.IO.Path.Combine(petha, mira));
            // ...and start a viewer.
            //Process.Start(mira);

            var prima = new Imprimir();
            prima.Viewer.Navigate(System.IO.Path.Combine(petha, mira));
            prima.Show();
            
            i++;
        }

        // create charges file
        public void CreateCharges(string nom, string prenom, string niveau)
        {
            // Create a new PDF document

            PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();

            document.Info.Title = "Created with PDFsharp";
            // Create an empty page
            PdfSharp.Pdf.PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            page.Size = PageSize.A5;
            page.Orientation = PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont fonti = new XFont("Verdana", 20, XFontStyle.Regular);
            XFont font = new XFont("Verdana", 16, XFontStyle.Regular);
            XFont fonta = new XFont("Verdana", 12, XFontStyle.Regular);

            // create an image
            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory, @"Bridges.png");
            XImage img = XImage.FromFile(fileLocation);
            const double tx = 190, ty = 100, txa = 1010;
            double wid = img.PixelWidth * 18 / img.HorizontalResolution;
            double hei = img.PixelHeight * 18 / img.HorizontalResolution;

            gfx.DrawImage(img, (tx - wid) / 2, (ty - hei) / 2, wid, hei);
            gfx.DrawImage(img, (txa - wid) / 2, (ty - hei) / 2, wid, hei);

            // Draw the text
            gfx.DrawString("Bridges Of Knowledge Center", font, XBrushes.Black, new XRect(0, -160, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Nom: " + nom, font, XBrushes.Black, new XRect(-(180 - nom.Length * 4), -50, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Prenom: " + prenom, font, XBrushes.Black, new XRect(-(168 - prenom.Length * 4), -10, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Niveau: " + niveau, font, XBrushes.Black, new XRect(-(172 - niveau.Length * 4), 30, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Paiement: 400 DZD", font, XBrushes.Black, new XRect(-162, 70, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Le : " + DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("fr-FR")), fonta, XBrushes.Black, new XRect(180, 120, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("à : " + DateTime.Now.ToString("hh:mm tt"), fonta, XBrushes.Black, new XRect(174, 140, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("© " + DateTime.Now.Year + ".", fonta, XBrushes.Black, new XRect(0, 200, page.Width, page.Height), XStringFormats.Center);
            XPen pen = new XPen(XColors.Navy, .5);
            gfx.DrawLine(pen, 70, 365, 520, 365);
            gfx.DrawString("Cité Hchachna en face CEM 1272 - Batna.", fonta, XBrushes.Black, new XRect(-135, 165, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Fixe : 033 28 30 67. Mobile : 0655 16 09 34", fonta, XBrushes.Black, new XRect(135, 165, page.Width, page.Height), XStringFormats.Center);
            XImage image = XImage.FromFile(@"face.png");
            XImage imagea = XImage.FromFile(@"twitter.png");
            XImage imageb = XImage.FromFile(@"insta.png");
            XImage imagec = XImage.FromFile(@"google.png");
            XImage imaged = XImage.FromFile(@"link.png");
            const double dx = 500, dy = 790, dxa = 550, dxb = 600, dxc = 650, dxd = 700;

            double width = image.PixelWidth * 4.5 / image.HorizontalResolution;

            double height = image.PixelHeight * 4.5 / image.HorizontalResolution;

            gfx.DrawImage(image, (dx - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imagea, (dxa - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imageb, (dxb - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imagec, (dxc - width) / 2, (dy - height) / 2, width, height);
            gfx.DrawImage(imaged, (dxd - width) / 2, (dy - height) / 2, width, height);

            // Save the document...
            FillExcelYear(nom, prenom, niveau, username);

            // Process open file dialog box results
            string mira = i + "spec.pdf";
            string petha = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            document.Save(System.IO.Path.Combine(petha, mira));
            // ...and start a viewer.
            //Process.Start(mira);
            var prima = new Imprimir();
            prima.Viewer.Navigate(System.IO.Path.Combine(petha, mira));
            prima.Show();
        }

        // create a preview
        public void CreatePreview()
        {
            XRect rect;
            XPen pen;
            PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();

            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfSharp.Pdf.PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            page.Size = PageSize.A5;
            page.Orientation = PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            double x = 50, y = 100;
            XFont fontH1 = new XFont("Times", 18, XFontStyle.Bold);

            XFont font = new XFont("Times", 12);

            XFont fontItalic = new XFont("Times", 12, XFontStyle.BoldItalic);

            double ls = font.GetHeight(gfx);

            // Draw some text

            gfx.DrawString("Create PDF on the fly with PDFsharp", fontH1, XBrushes.Black, x, x);

            gfx.DrawString("With PDFsharp you can use the same code to draw graphic, " +
            "text and images on different targets.", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("The object used for drawing is the XGraphics object.",
            font, XBrushes.Black, x, y);
            y += 2 * ls;
            // Draw an arc

            pen = new XPen(XColors.Red, 4);

            pen.DashStyle = XDashStyle.Dash;

            gfx.DrawArc(pen, x + 20, y, 100, 60, 150, 120);

            // Draw a star
            XGraphicsState gs = gfx.Save();

            gfx.TranslateTransform(x + 140, y + 30);

            for (int idx = 0; idx < 360; idx += 10)

            {

                gfx.RotateTransform(10);

                gfx.DrawLine(XPens.DarkGreen, 0, 0, 30, 0);

            }

            gfx.Restore(gs);

            // Draw a rounded rectangle
            rect = new XRect(x + 230, y, 100, 60);
            pen = new XPen(XColors.DarkBlue, 2.5);
            XColor color1 = XColor.FromKnownColor(KnownColor.DarkBlue);
            XColor color2 = XColors.Red;
            XLinearGradientBrush lbrush = new XLinearGradientBrush(rect, color1, color2,
            XLinearGradientMode.Vertical);
            gfx.DrawRoundedRectangle(pen, lbrush, rect, new XSize(10, 10));

            // Draw a pie
            pen = new XPen(XColors.DarkOrange, 1.5);
            pen.DashStyle = XDashStyle.Dot;
            gfx.DrawPie(pen, XBrushes.Blue, x + 360, y, 100, 60, -130, 135);

            // Draw some more text
            y += 60 + 2 * ls;
            gfx.DrawString("With XGraphics you can draw on a PDF page as well as on any System.Drawing.Graphics object.", font, XBrushes.Black, x, y);
            y += ls * 1.1;
            gfx.DrawString("Use the same code to", font, XBrushes.Black, x, y);
            x += 10;
            y += ls * 1.1;
            gfx.DrawString("• draw on a newly created PDF page", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("• draw above or beneath of the content of an existing PDF page", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("• draw in a window", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("• draw on a printer", font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("• draw in a bitmap image", font, XBrushes.Black, x, y);
            x -= 10;
            y += ls * 1.1;
            gfx.DrawString("You can also import an existing PDF page and use it like an image, e.g. draw it on another PDF page.", font, XBrushes.Black, x, y);
            y += ls * 1.1 * 2;
            gfx.DrawString("Imported PDF pages are neither drawn nor printed; create a PDF file to see or print them!", fontItalic, XBrushes.Firebrick, x, y);
            y += ls * 1.1;
            gfx.DrawString("Below this text is a PDF form that will be visible when viewed or printed with a PDF viewer.", fontItalic, XBrushes.Firebrick, x, y);
            y += ls * 1.1;
            XGraphicsState state = gfx.Save();
            XRect rcImage = new XRect(100, y, 100, 100 * Math.Sqrt(2));
            gfx.DrawRectangle(XBrushes.Snow, rcImage);
            string kara = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            gfx.DrawImage(XPdfForm.FromFile(System.IO.Path.Combine(kara, "slother.pdf")), rcImage);
            gfx.Restore(state);
            
        }

        // Fill an excel file
        private int Compter()
        {
            Workbook loka = new Workbook();
            string patha = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            loka.LoadFromFile(System.IO.Path.Combine(patha, ReadName()));
            return loka.Worksheets[0].Rows.Length - 3;
        }

        // write the file name in a text file
        public void WriteName(string hombre)
        {
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/modpas.txt"), true))
            {
                outputFile.WriteLine(hombre);
            }
        }

        // write the user name in a text file
        public void WriteUserName(string hombre)
        {
            string patha = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/Users.txt");
            File.WriteAllText(patha, string.Empty);
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/Users.txt"), true))
            {
                outputFile.WriteLine(hombre);
            }
        }

        // read a file name from a text file
        private string ReadName()
        {
            string nombre = "";

            using (StreamReader sr = new StreamReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/modpas.txt")))
            {
                try
                {
                    while (sr.Peek() >= 0)
                    {
                        nombre = sr.ReadLine();
                    }
                }
                catch
                {
                    MessageBox.Show("File is void ! Please create a file first.");
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }

            }
            return nombre;
        }

        // read a user name from a text file
        private string ReadUserName()
        {
            string nombre = "";

            using (StreamReader sr = new StreamReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex/Users.txt")))
            {
                try
                {
                    while (sr.Peek() >= 0)
                    {
                        nombre = sr.ReadLine();
                    }
                }
                catch
                {
                    MessageBox.Show("File is void ! Please create a file first.");
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }

            }
            return nombre;
        }

        // fill an excel file with infos
        // fill the file with month payment
        public void fillExcelFile(string nom, string prenom, string prof, string niveau, string matiere, string groupe, string prix, string userna)
        {
            Workbook wb = new Workbook();
            string pluto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            wb.LoadFromFile(System.IO.Path.Combine(pluto, ReadName()));
            Worksheet ws = wb.Worksheets[0];

            for (int s = 4; s < ws.Rows.Length; s++)
            {
                if (ws.Range["B" + s].Value == "")
                {
                    string cur = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                    ws.Range["B" + s].Value = s - 3 + ""; // #
                    ws.Range["C" + s].Value = nom; // nom
                    ws.Range["D" + s].Value = prenom; // prenom
                    ws.Range["E" + s].Value = niveau; // prof
                    ws.Range["F" + s].Value = matiere; // matiere
                    ws.Range["G" + s].Value = prof; // niveau
                    ws.Range["H" + s].Value = groupe; // groupe
                    ws.Range["I" + s].Value = prix;// prix
                    ws.Range["J" + s].Value = cur;// date
                    ws.Range["K" + s].Value = ReadUserName(); // username
                    break;
                }
                else
                {
                    continue;
                }
            }

            wb.SaveToFile(System.IO.Path.Combine(pluto, ReadName()));
        }

        //fill the file with the year payment
        public void FillExcelYear(string nom, string prenom, string niveau, string userna)
        {
            Workbook wb = new Workbook();
            string pluto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            wb.LoadFromFile(System.IO.Path.Combine(pluto, ReadName()));
            Worksheet ws = wb.Worksheets[1];

            for (int s = 4; s < ws.Rows.Length; s++)
            {
                if (ws.Range["B" + s].Value == "")
                {
                    string cur = DateTime.Now.Date.ToString("dd/MM/yyyy", new CultureInfo("fr-FR"));
                    ws.Range["B" + s].Value = s - 3 + ""; // #
                    ws.Range["C" + s].Value = nom; // nom
                    ws.Range["D" + s].Value = prenom; // prenom
                    ws.Range["E" + s].Value = niveau; // niveau
                    ws.Range["F" + s].Value = "400";// prix
                    ws.Range["G" + s].Value = cur;// date
                    ws.Range["H" + s].Value = ReadUserName(); // username
                    break;
                }
                else
                {
                    continue;
                }
            }

            wb.SaveToFile(System.IO.Path.Combine(pluto, ReadName()));
        }

        // Create an Excel File
        // Notes
        /***
         *this function still lacks charts and some case management 
         * 
         * 
         * 
         ***/
        private void CreateExcel_Click(object sender, RoutedEventArgs e)
        {
            CreateExcels();
        }

        // Delete every pdf file in the Printex Folder
        private void Vider_Click(object sender, RoutedEventArgs e)
        {
            string pluto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            string[] files = System.IO.Directory.GetFiles(pluto, "*.pdf");

            foreach (string file in files)
            {
                System.IO.File.Delete(file);
            }
        }

        // Open the Excel workbook that stores the data
        private void Opena_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string pluto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Printex");
            System.Diagnostics.Process.Start(System.IO.Path.Combine(pluto, ReadName()));
        }

        private void Clerck_Click(object sender, RoutedEventArgs e)
        {
            var plus = new Account();
            plus.Show();
        }

        private void SoftInfo_Click(object sender, RoutedEventArgs e)
        {
            new AboutSoft().Show();
        }

        private void Kateb_Click(object sender, RoutedEventArgs e)
        {
            new AboutMe().Show();
        }

        private void switch_Click(object sender, RoutedEventArgs e)
        {
            new Account().Show();
        }

        // close button
        private void Closa_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // reset the form
        private void ResetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                new PayerMois().resetta();
            }
        }

    }

}

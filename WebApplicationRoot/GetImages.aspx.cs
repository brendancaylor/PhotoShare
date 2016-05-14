using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationRoot
{
    public partial class GetImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAllImages();
        }

        public List<string> ImageList; // Creating a list array
        public void GetAllImages()
        {
            ImageList = new List<string>();

            int iLoop = 0;
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_8966.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_9456.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_8991.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/Hands1.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_9431.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_05801.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_8739.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_15741.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_0369.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_0484.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_13501.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_02701.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1107.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1072.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1151.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_04021.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_6758.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_7159.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1142.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_6978.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_6800.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_7314.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_19412.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1744.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_10571.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1587.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1751.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_2448.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1015a1.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1633.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1779.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1066.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_2599-22.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_2592-21.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_2475-23.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1771.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_23952.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_22802.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_21771.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_19161.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1492-2b1.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1435.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_13611.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_12991.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_12282.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_12021.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_1186a1.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_10512.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09881.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09632.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09611.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09442.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09422.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_0921-21.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09092.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_09002.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_0878.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_08702.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_08352.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_07572.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_05791.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_05741.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_05702.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_0478a1.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_04572.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_04001.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_03741.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_03671.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_03573.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_03262.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/IMG_01852.jpg");
            ImageList.Add("http://www.mi-trueimage.co.uk/wp-content/uploads/2013/08/0522.jpg");

            /*
            // Declaring 'x' as a new WebClient() method
            WebClient x = new WebClient();
            // Setting the URL, then downloading the data from the URL.
            string source = x.DownloadString(@"http://www.mi-trueimage.co.uk/");
            // Declaring 'document' as new HtmlAgilityPack() method
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            // Loading document's source via HtmlAgilityPack
            document.LoadHtml(source);
            // For every tag in the HTML containing the node img.

            
            foreach (var link in document.DocumentNode
                                 .Descendants("img")
                                 .Select(o => o.Attributes["src"]))
            
             */
            foreach (var url in ImageList)
            {
                // Storing all links found in an array. You can declare this however you want.
                //ImageList.Add(link.Attribute["src"].Value.ToString());

                iLoop++;
                try
                {
                    WebClient wc = new WebClient();
                    wc.DownloadFile(url, @"C:\temp\WebImages\" + iLoop + ".jpg");
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    
                }

            }
        }

    }
}
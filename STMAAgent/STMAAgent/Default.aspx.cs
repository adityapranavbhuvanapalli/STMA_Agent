using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;

namespace STMAAgent
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            pnlShowUser.Visible = false;
            pnlToken.Visible = false;
           

            if (!IsPostBack)
            {
                string url = ConfigurationManager.AppSettings["STMAWBURL"].ToString();

                url = url + "?op=LoadServices";

                HttpWebRequest request = CreateWebRequest(url);
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                        <LoadServices xmlns=""http://tempuri.org/"">
        
                        </LoadServices>
                    </soap:Body>
                    </soap:Envelope>");



                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        XmlDocument ele1 = new XmlDocument();
                        XmlDocument ele2 = new XmlDocument();
                        doc.LoadXml(soapResult);

                        XmlNodeList elemList = doc.GetElementsByTagName("ServiceInfo");
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            string s1 = elemList[i].InnerXml.ToString();
                            s1 = "<Services>" + s1 + "</Services>";
                            ele1.LoadXml(s1);
                            string sid;
                            string sname;
                            foreach (XmlNode xmlNode in ele1.ChildNodes)
                            {

                                sid = xmlNode["ServiceID"].InnerText;
                                sname = xmlNode["ServiceName"].InnerText;
                                ddlServices.Items.Add(new ListItem(sname, sid));

                            }

                        }


                    }
                }
            }

        }

        public HttpWebRequest CreateWebRequest(string url)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            lblNoUser.Text = "";
            pnlToken.Visible = false;
            string mobile = txtMobile.Text;
            //LoadUsers

            string url = ConfigurationManager.AppSettings["STMAWBURL"].ToString();

            url = url + "?op=LoadUsers";

            HttpWebRequest request = CreateWebRequest(url);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                        <LoadUsers xmlns=""http://tempuri.org/"">"
                + "<Mobile>" + mobile + "</Mobile>" +
                        "</LoadUsers></soap:Body></soap:Envelope>");



            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XmlDocument doc = new XmlDocument();
                    XmlDocument ele1 = new XmlDocument();
                    //XmlDocument ele2 = new XmlDocument();
                    doc.LoadXml(soapResult);

                    XmlNodeList elemList = doc.GetElementsByTagName("LoadUsersResult");
                    if (elemList[0].InnerText != "0")
                    {
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            string s1 = elemList[i].InnerXml.ToString();

                            ele1.LoadXml(s1);
                            string UserID;
                            string UserName;
                            string dob;
                            string gender;
                            string address;
                            string mobile1;
                            foreach (XmlNode xmlNode in ele1.ChildNodes)
                            {

                                UserID = xmlNode["UserID"].InnerText;
                                UserName = xmlNode["UserName"].InnerText;
                                dob = xmlNode["dob"].InnerText;
                                gender = xmlNode["gender"].InnerText;
                                address = xmlNode["address"].InnerText;
                                mobile1 = xmlNode["mobile"].InnerText;

                                lblUserID.Text = UserID;
                                lblUserName.Text = UserName;
                                lblDob.Text = dob;
                                lblGender.Text = gender;
                                lblAddress.Text = address;
                                lblMobile.Text = mobile1;
                                hdnUserID.Value = UserID;
                            }



                        }

                        pnlShowUser.Visible = true;

                    }
                    else
                    {
                        lblNoUser.Text = "<font color='red'>Mobile is not found in our system. Please call AAM Helpdesk to register.</font>";
                    }



                }
            }
        }

        protected void btnGenToken_Click(object sender, EventArgs e)
        {
            lblTokenMsg.Text = "";
            string ServiceId = ddlServices.SelectedValue.ToString();
            string userId = hdnUserID.Value.ToString();

            string url = ConfigurationManager.AppSettings["STMAWBURL"].ToString();

            url = url + "?op=GenerateServiceToken";

            HttpWebRequest request = CreateWebRequest(url);
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                        <GenerateServiceToken xmlns=""http://tempuri.org/"">"
                + "<ServiceID>" + ServiceId + "</ServiceID>" 
                 +"<PatientID>" + userId + "</PatientID>" 
                  +"<UserID>" + 2000 + "</UserID>" +
                        "</GenerateServiceToken></soap:Body></soap:Envelope>");



            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    XmlDocument doc = new XmlDocument();
                    XmlDocument ele1 = new XmlDocument();
                    //XmlDocument ele2 = new XmlDocument();
                    doc.LoadXml(soapResult);
                    XmlNodeList elemList1 = doc.GetElementsByTagName("ServiceTokenID");
                    string tno = elemList1[0].InnerText.ToString();

                    if (tno == "0")
                    {
                        pnlToken.Visible = false;
                        lblTokenMsg.Text = "<font color='red'>Token Service has temporarily stopped. Please try after some time.</font>";
                    }
                    else
                    {
                        XmlNodeList elemList = doc.GetElementsByTagName("ServiceTokenID");
                        lblTokenNo.Text = elemList[0].InnerText.ToString();
                        elemList = doc.GetElementsByTagName("ServiceName");
                        lblSname.Text = elemList[0].InnerText.ToString();

                        elemList = doc.GetElementsByTagName("QueueNo");
                        lblQNo.Text = elemList[0].InnerText.ToString();
                        elemList = doc.GetElementsByTagName("BayArea");
                        lblBay.Text = elemList[0].InnerText.ToString();
                         elemList = doc.GetElementsByTagName("Rooms");
                        lblRooms.Text = elemList[0].InnerText.ToString();
                         elemList = doc.GetElementsByTagName("ExpectedWaitingTime");
                        lblEWT.Text = elemList[0].InnerText.ToString();
                        pnlShowUser.Visible = false;
                        pnlToken.Visible = true;
                    }
                   

                }
            }
        }
    }
}
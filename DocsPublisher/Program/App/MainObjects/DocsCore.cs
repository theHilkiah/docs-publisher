using Sgml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DocsPublisher.Program.App.MainObjects
{
    class DocsCore
    {

        XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Parse,
            XmlResolver = new XmlUrlResolver()
        };

        public dynamic LoadXMLwithDTD(string inputUri)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(inputUri, xmlReaderSettings))
                {
                    return XDocument.Load(reader);
                }
            }
            catch (Exception e)
            {
                return MessageBox.Show(e.Message, "Loading XML With DTD Failed");
            }

        }

        public dynamic TransformXMLWithXSLT(string style, string input, string output = null)
        {
            try
            {
                string finalOutput = output ?? "transformed-" + input;
                var XslTransform = new XslCompiledTransform();
                XslTransform.Load(style);
                XslTransform.Transform(input, finalOutput);
                return finalOutput;
            }
            catch (Exception ex)
            {
                return MessageBox.Show(ex.Message, "XSLT Transformation");
            }
        }

        public string ConvertSGMtoXML(dynamic sgmlInput, string sgmlDTD = null, string entities = null)
        {
            try
            {
                Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
                sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;

                //If dtd is provided, we can use it to parse the xml
                if (sgmlDTD != null)
                {
                    sgmlReader.IgnoreDtd = false;
                    sgmlReader.InternalSubset = sgmlDTD;
                }

                //Check what type of sgmlInput is provided
                if (sgmlInput is TextWriter)
                    sgmlReader.InputStream = sgmlInput;
                else
                    sgmlReader.Href = sgmlInput;


                XDocument xmlDoc = XDocument.Load(sgmlReader);

                //Check to see if there is a doctype declaration in the xmldoc
                if (xmlDoc.DocumentType == null)
                {
                    string rootName = xmlDoc.Root.Name.ToString();
                    xmlDoc.Root.AddBeforeSelf(new XDocumentType(rootName, "", "", ""));
                }

                //If entities file is provided, read it and add it to the xml doc
                if (entities != null)
                {
                    string docTypeEntities = File.ReadAllText(entities);

                    if (docTypeEntities.Contains("<!DOCTYPE") || docTypeEntities.Contains("]>"))
                        docTypeEntities = Regex.Replace(docTypeEntities, @"<!DOCTYPE(.+?)\[|\]\>", "");

                    xmlDoc.DocumentType.InternalSubset = docTypeEntities;
                }

                //Convert the xml to string in order to fix it
                string xmlString = xmlDoc.ToString();

                if (xmlString.Contains("</revst>")
                    || xmlString.Contains("</revend>")
                    || xmlString.Contains("</cocst>")
                    || xmlString.Contains("</revst>"))
                {
                    xmlString = xmlString.Replace("<revst>", "<revst/>").Replace("</revst>", "")
                                         .Replace("<revend>", "<revend/>").Replace("</revend>", "")
                                         .Replace("<cocst>", "<cocst/>").Replace("</cocst>", "")
                                         .Replace("<cocend>", "<cocend/>").Replace("</cocend>", "");
                }
                return xmlString;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unable to transform SGML to XML");
                return e.Message;
            }
        }

        protected internal static void AutoCloseTags(SgmlReader reader, XmlWriter writer)
        {
            object msgBody = reader.NameTable.Add("MSGBODY");

            object previousElement = null;
            Stack elementsWeAlreadyEnded = new Stack();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        previousElement = reader.LocalName;
                        writer.WriteStartElement(reader.LocalName);
                        break;
                    case XmlNodeType.Text:
                        if (String.IsNullOrEmpty(reader.Value) == false)
                        {
                            writer.WriteString(reader.Value.Trim());
                            if (previousElement != null && !previousElement.Equals(msgBody))
                            {
                                writer.WriteEndElement();
                                elementsWeAlreadyEnded.Push(previousElement);
                            }
                        }
                        else Debug.Assert(true, "big problems?");
                        break;
                    case XmlNodeType.EndElement:
                        if (elementsWeAlreadyEnded.Count > 0
                            && Object.ReferenceEquals(elementsWeAlreadyEnded.Peek(),
                               reader.LocalName))
                        {
                            elementsWeAlreadyEnded.Pop();
                        }
                        else
                        {
                            writer.WriteEndElement();
                        }
                        break;
                    default:
                        writer.WriteNode(reader, false);
                        break;
                }
            }
        }
    }
}

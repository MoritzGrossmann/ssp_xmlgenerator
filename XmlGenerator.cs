using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ssp_xmlgenerator
{
    public class XmlGenerator 
    {
        private const int MaxProductionOrders = 10;
        private const string ARRAY_OF_CUSTOMERS_XML_PROPERTY = "ArrayOfCustomers";

        private const string CUSTOMER_XML_PROPERTY = "Customer";
        private const string NAME_XML_PROPERTY = "Name";

        private const string ID_XML_PROPERTY = "Id";

        private const string CUSTOMER_ORDERS_XML_PROPERTY = "CustomerOrders";

        private const string CUSTOMER_ORDER_XML_PROPERTY = "CustomerOrder";

        private const string CONTACT_XML_PROPERTY = "Contact";

        private const string STREET_XML_PROPERTY = "Street";

        private const string POSTCODE_XML_PROPERTY = "PostCode";

        private const string HOUSENUMBER_XML_PROPERTY = "HouseNumber";

        private const string CITY_XML_PROPERTY = "City";

        private const string TELEFON_PRIVATE_XML_PROPERTY = "Private";

        private const string TELEFON_MOBILE_XML_PROPERTY = "Mobile";

        private const string EMAIL_XML_PROPERTY = "Email";

        private const string FAX_XML_PROPERTY = "Fax";

        private const string DATETIME_XML_PROPERTY = "DateTime";

        private const string PRODUCTION_ORDERS_XML_PROPERTY = "ProductionOrders";

        private const string PRODUCTION_ORDER_XML_PROPERTY = "ProductionOrder";

        private const string COUNTRY_XML_PROPERTY = "Country";

        private RootObject _rootObject;

        private StreamWriter _writer;

        private Stack<string> _stack;

        private Random _random;

        public XmlGenerator(string fileName, RootObject rootObject) 
        {
            _writer = new StreamWriter(fileName);
            _rootObject = rootObject;
            _stack = new Stack<string>();
            _random = new Random();
        }

        public async Task WriteFile() 
        {
            int i = 0;

            int k = 0;

            writeTag(ARRAY_OF_CUSTOMERS_XML_PROPERTY, "xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/OLDB.Spa.Models.Api\"");
            newLine();

            _rootObject.results.ForEach(person => {
                writeTag(CUSTOMER_XML_PROPERTY);
                newLine();

                writeTag(ID_XML_PROPERTY);
                writeValue($"{++i}");
                writeFromStack(false);

                writeTag(NAME_XML_PROPERTY);
                writeValue($"{person.name.first} {person.name.last}");
                writeFromStack(false);

                writeTag(CONTACT_XML_PROPERTY);
                newLine();

                writeTag(STREET_XML_PROPERTY);
                writeValue(person.location.street.Substring(5));
                writeFromStack(false);

                writeTag(HOUSENUMBER_XML_PROPERTY);
                writeValue($"{person.location.street.Substring(0,4)}");
                writeFromStack(false);

                writeTag(POSTCODE_XML_PROPERTY);
                writeValue($"{person.location.postcode}");
                writeFromStack(false);

                writeTag(CITY_XML_PROPERTY);
                writeValue($"{person.location.city}");
                writeFromStack(false);

                writeTag(COUNTRY_XML_PROPERTY);
                writeValue($"{person.location.state}");
                writeFromStack(false);

                writeTag(EMAIL_XML_PROPERTY);
                writeValue($"{person.email}");
                writeFromStack(false);

                writeTag(TELEFON_PRIVATE_XML_PROPERTY);
                writeValue($"{person.phone}");
                writeFromStack(false);

                writeTag(TELEFON_MOBILE_XML_PROPERTY);
                writeValue($"{person.cell}");
                writeFromStack(false);

                writeTag(FAX_XML_PROPERTY);
                writeValue($"{person.phone}");
                writeFromStack(false);

                writeFromStack(true);


                writeTag(CUSTOMER_ORDERS_XML_PROPERTY);

                newLine();

                for (int j = 0; j < _random.Next(1,10); j++)
                {

                    writeTag(CUSTOMER_ORDER_XML_PROPERTY);

                    newLine();

                    writeTag(ID_XML_PROPERTY);
                    writeValue($"{++k}");
                    writeFromStack(false);

                    writeTag(DATETIME_XML_PROPERTY);
                    var dateTime = getRandomDateTime();
                    writeValue($"{dateTime.Year.ToString("D4")}-{dateTime.Month.ToString("D2")}-{dateTime.Day.ToString("D2")}T{_random.Next(0,23).ToString("D2")}:{_random.Next(0,59).ToString("D2")}:{_random.Next(0,59).ToString("D2")}");
                    writeFromStack(false);

                    writeTag(PRODUCTION_ORDERS_XML_PROPERTY);

                    newLine();

                    for (int l = 0; l < _random.Next(1,10);l++) 
                    {
                        writeTag(PRODUCTION_ORDER_XML_PROPERTY);
                        writeValue($"{_random.Next(1,MaxProductionOrders)}");
                        writeFromStack(false);
                    }

                    writeFromStack(true);
                    writeFromStack(true);
                }

                writeFromStack(true);

                writeFromStack(true);

            });

            writeFromStack(true);

            _writer.Flush();
            _writer.Close();
        }

        private void writeTag(string tag)
        {
            var stringbuilder = new StringBuilder("");

            for (int i = 0; i < _stack.Count; i++) {
                stringbuilder.Append("\t");
            }

            stringbuilder.Append($"<{tag}>");
            _writer.Write(stringbuilder.ToString());
            _stack.Push(tag);
        }

        private void writeTag(string tag, string properties) {
            _writer.Write($"<{tag} {properties}>");
            _stack.Push(tag);
        }

        private void writeValue(string value) {
            _writer.Write(value);
        }

        private void newLine() {
            _writer.WriteLine();
        }

        private void writeFromStack(bool withTabs) 
        {
            var stringbuilder = new StringBuilder("");

            if (withTabs) {
                for (int i = 0; i < _stack.Count-1; i++) {
                    stringbuilder.Append("\t");
                }
            }

            stringbuilder.Append($"</{_stack.Pop()}>");
            _writer.Write(stringbuilder.ToString());
            newLine();
        }

        private DateTime getRandomDateTime() 
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;           
            return start.AddDays(_random.Next(range));
        }
    }
}
namespace DB_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<person> people = new List<person>();
            string per = File.ReadAllText(@"..\..\..\json1.json");
            string addr = File.ReadAllText(@"..\..\..\XMLFile1.xml");
            var per_data = JsonSerializer.Deserialize<JsonObject>(per);
            XmlDocument jdesc_data = new XmlDocument();
            jdesc_data.LoadXml(addr);
            for (int i = 0; i < jdesc_data.ChildNodes.Count; i++)
            {
                person person = new person();
                person.name = (string)per_data["person"][i]["name"];
                person.email = (string)per_data["person"][i]["email"];
                person.address =(string) per_data["person"][i]["address"];
                XmlNode xmlNode = jdesc_data.SelectNodes("job_pos/job_desc").OfType<XmlElement>().ElementAt(i);
                person.job_Desc = new job_desc { job_title = xmlNode["job_title"].InnerText, sal_amt = (float)Convert.ToDouble(xmlNode["sal_amt"].InnerText) };                
                people.Add(person);
            }
            Assert.AreEqual("john doe", people.ElementAt(0).name);
            Assert.AreEqual("jd@email.com", people.ElementAt(1).email);
            Assert.AreEqual("avenue", people.ElementAt(1).address);
            //Assert.AreEqual(96148, people.ElementAt(1).job_Desc.sal_amt);
        }
    }
    public class person
    {
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public job_desc job_Desc {  get; set; }
    }
    public class job_desc 
    {
        public string job_title {  get; set; }
        public float sal_amt {  get; set; }
    }    
}
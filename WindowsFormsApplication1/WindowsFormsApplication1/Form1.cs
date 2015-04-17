using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        int index = 0;
        int i = 0;
            
        public Form1()
        {
            dt.Clear();
            dt.Columns.Add("ItemID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("Rarity");
            dt.Columns.Add("Level");
            dt.Columns.Add("Value");
            dt.Columns.Add("Game_type1");
            dt.Columns.Add("Game_type2");
            dt.Columns.Add("Game_type3");
            dt.Columns.Add("Game_type4");
            dt.Columns.Add("Game_type5");
            dt.Columns.Add("Game_type6");
            dt.Columns.Add("Type");
            dt.Columns.Add("Details_type");
            dt.Columns.Add("Details_weight_class");
            dt.Columns.Add("Details_defense");
            dt.Columns.Add("Details_min_power");
            dt.Columns.Add("Details_max_power");
            dt.Columns.Add("Buff");
            dt.Columns.Add("Bonus1");
            dt.Columns.Add("Bonus2");
            dt.Columns.Add("Bonus3");
            dt.Columns.Add("Bonus4");
            dt.Columns.Add("Bonus5");
            dt.Columns.Add("Bonus6");
             dt.Columns.Add("Upgrade_attribute1");
            dt.Columns.Add("Upgrade_modifier1");

            dt.Columns.Add("Upgrade_attribute2");
            dt.Columns.Add("Upgrade_modifier2");

            dt.Columns.Add("Upgrade_attribute3");
            dt.Columns.Add("Upgrade_modifier3");

            dt.Columns.Add("Upgrade_attribute4");
            dt.Columns.Add("Upgrade_modifier4");

            dt.Columns.Add("Upgrade_attribute5");
            dt.Columns.Add("Upgrade_modifier5");

            dt.Columns.Add("Upgrade_attribute6");
            dt.Columns.Add("Upgrade_modifier6");

            dt.Columns.Add("Upgrade_attribute7");
            dt.Columns.Add("Upgrade_modifier7");

            dt.Columns.Add("Upgrade_attribute8");
            dt.Columns.Add("Upgrade_modifier8");

            dt.Columns.Add("Upgrade_attribute9");
            dt.Columns.Add("Upgrade_modifier9");

            dt.Columns.Add("Restrictions1");
            dt.Columns.Add("Restrictions2");
            dt.Columns.Add("Restrictions3");
           

       

            dt.TableName = "myTable";

            InitializeComponent();
            start();
        }

       

           
        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
          string output  =e.Result;
         
          int[] obj = JsonConvert.DeserializeObject<int[]>(output);
          textBox2.Text = obj.Length.ToString();
          getData(obj);
        

        }

       void getData(int [] obj){
           
           while (index < 40800)
           {
               string myList="";

               try
               {
                   for (int z=0;z<200;z++){
                   
                       if (z==0)
                           myList=obj[(z+index)].ToString();
                       else
                       myList=myList+","+obj[(z+index)].ToString();
                   }
                    

                   Uri baseURI = new Uri("https://api.guildwars2.com/v2/items?ids="+myList);
                  
                   WebClient wc2 = new WebClient();
                   wc2.DownloadStringAsync(baseURI);
                   wc2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted2);
                   
               }
               catch { }
               index=index+200;
           }
           
          
        }

        void wc_DownloadStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
            i = i + 200;
           
            Item [] obj = JsonConvert.DeserializeObject<Item []>(e.Result);
            for (int y = 0; y < obj.Length; y++)
            {
                DataRow _row = dt.NewRow();
                _row["Name"] = obj[y].name;
                _row["Description"] = obj[y].description;
                _row["ItemID"] = obj[y].id;
                _row["Rarity"] = obj[y].rarity;
                _row["Value"] = obj[y].vendor_value;
                _row["Level"] = obj[y].level;
                _row["Type"] = obj[y].type;
                if (obj[y].details != null)
                {
                    _row["Details_type"] = obj[y].details.type;
                    _row["Details_weight_class"] = obj[y].details.weight_class;
                    _row["Details_defense"] = obj[y].details.defense;
                    _row["Details_min_power"] = obj[y].details.min_power;
                    _row["Details_max_power"] = obj[y].details.max_power;

                    if (obj[y].details.bonuses != null)
                    {
                        for (int x = 0; x < obj[y].details.bonuses.Length; x++)
                        {
                            _row["Bonus" + (x + 1)] = obj[y].details.bonuses[x];

                        }

                    }

                    if (obj[y].details.infix_upgrade != null)
                    {
                        for (int x = 0; x < obj[y].details.infix_upgrade.attributes.Length; x++)
                        {
                            _row["Upgrade_attribute" + (x + 1)] = obj[y].details.infix_upgrade.attributes[x].attribute;
                            _row["Upgrade_modifier" + (x + 1)] = obj[y].details.infix_upgrade.attributes[x].modifier;


                        }
                        if (obj[y].details.infix_upgrade.buff != null)
                        {
                            _row["Buff"] = obj[y].details.infix_upgrade.buff.description;
                           

                        }

                    }
                }

                for (int j = 0; j < obj[y].game_types.Length; j++)
                {
                    _row["Game_type" + (j + 1)] = obj[y].game_types[j];
                }

                for (int j = 0; j < obj[y].restrictions.Length; j++)
                {
                    _row["Restrictions" + (j + 1)] = obj[y].restrictions[j];
                }

               textBox2.Text = "i value"+i.ToString();

                dt.Rows.Add(_row);
                if (i == 40800)
                {
                    dt.WriteXml("dtDataxml.xml");
                    textBox2.Text = "im done";
                }
            }
        }
       

        private void start()
        {

            try
            {
                textBox2.Text = "here";
                Uri baseURI = new Uri("https://api.guildwars2.com/v2/items/");
                    WebClient wc = new WebClient();
                    wc.DownloadStringAsync(baseURI);
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                }
                
            
            catch { }
        }



        private void start1()
        {

            try
            {
                textBox2.Text = "here";
                Uri baseURI = new Uri("https://api.guildwars2.com/v2/items?ids=37907,37911");
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(baseURI);
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted2);
            }


            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        
    }

}
   

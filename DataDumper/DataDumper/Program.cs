using Aerospike.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DataDumper
{
    class Program
    {
        static void Main(string[] args)
        {
            AerospikeClient client = new AerospikeClient("18.235.70.103", 3000);
            string folderPath = "C:/Users/pgoel/Desktop/tt.csv";
            string nameSpace = "AirEngine";
            string setName = "PallviGoyals";
            DataTable dt = new DataTable();
            var c = 0;
            using (StreamReader sr = new StreamReader(folderPath))
            {
            string[] headers = sr.ReadLine().Split(',');

                //string[] headers = Regex.Split(sr.ReadLine(), ",");
                // sr.SetDelimiters(new string[] { "," });

                foreach (string header in headers)
                {
                    c++;
                    if (c == 18)
                    {
                        dt.Columns.Add("New_Date");

                    }
                    else

                        dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    if (c == 10)
                        break;
                    //string[] rows = Regex.Split(sr.ReadLine(), ",");
                    //sr.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                    sr.ReadLine().Replace(',', ' ');
                    string[] rows = sr.ReadLine().Split(new string[] { ",  " }, StringSplitOptions.None);

                    //     string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();

                        c++;

                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }

            c = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (c == 10)
                    break;
                c++;
                //   Console.WriteLine();
                //foreach (DataColumn col in dt.Columns)
                var key = new Key(nameSpace, setName, row[16].ToString());
                //   string key = new Key(nameSpace,setName, keyValue);


                Console.WriteLine(row[0].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("author", row[0].ToString()) });

                Console.WriteLine(row[1].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("content", row[1].ToString()) });

                Console.WriteLine(row[2].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("region", row[2].ToString()) });

                Console.WriteLine(row[3].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("language", row[3].ToString()) });

                Console.WriteLine(row[4].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("tweet_date", row[4].ToString()) });

                Console.WriteLine(row[5].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("tweet_time", row[5].ToString()) });

                Console.WriteLine(row[6].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("year", row[6].ToString()) });

                Console.WriteLine(row[7].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("hour", row[7].ToString()) });

                Console.WriteLine(row[8].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("month", row[8].ToString()) });

                Console.WriteLine(row[9].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("minute", row[9].ToString()) });

                Console.WriteLine(row[10].ToString() + " ");

                client.Put(new WritePolicy(), key, new Bin[] { new Bin("following", row[10].ToString()) });
                Console.WriteLine(row[11].ToString() + " ");
                Console.WriteLine(row[12].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("followers", row[10].ToString()) });

                Console.WriteLine(row[13].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("post_url", row[12].ToString()) });

                Console.WriteLine(row[14].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("post_type", row[13].ToString()) });

                Console.WriteLine(row[15].ToString() + " "); Console.WriteLine(row[14].ToString() + " ");

                Console.WriteLine(row[16].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("retweet", row[15].ToString()) });


                Console.WriteLine(row[17].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("tweetid", row[16].ToString()) });

                Console.WriteLine(row[18].ToString() + " ");
                client.Put(new WritePolicy(), key, new Bin[] { new Bin("authorid", row[17].ToString()) });
                //client.Put(new WritePolicy(), key, new Bin[] { new Bin("account_category", row[18].ToString()) });
                //     client.Put(new WritePolicy(), key, new Bin[] { new Bin("new_june", row[0].ToString()) });

                Console.WriteLine();
                //    string row[0].ToString() = row[17].ToString();


            }
            Console.ReadKey();
        }
    }
}


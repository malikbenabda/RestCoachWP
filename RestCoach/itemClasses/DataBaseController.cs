
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;
using System.IO;

namespace RestCoach
{
    class DataBaseController
    {
        /**
             public static void createTable()
                {
            App.conn= new SQLiteConnection("Storage.db");
                    {
                //TODO alter table creation!
                string sql = @"CREATE TABLE IF NOT EXISTS WorkSessions
                        ( `Id` INT NOT NULL AUTO_INCREMENT ,
                            `StartWorkTime` DATETIME NOT NULL , 
                            `EndWorkTime` DATETIME NOT NULL ,
                             `DurationInMinutes` INT NOT NULL ,
                            `TimeLeftInMinutes` INT NOT NULL ,
                            `percentageOfWork` INT NOT NULL ,
                            `Title` VARCHAR NOT NULL ,
                            `Description` VARCHAR NOT NULL ,
                            `Stressed` BOOLEAN NOT NULL , 
                            `Tired` BOOLEAN NOT NULL ,
                            `Comfortable` BOOLEAN NOT NULL ,
                            `Noise` BOOLEAN NOT NULL , `OnDesk` BOOLEAN NOT NULL ,
                            `overallState` INT NOT NULL , `stressImage` VARCHAR NULL ,
                            `tiredImage` VARCHAR NULL , `comforImage` VARCHAR NULL ,
                            `noiseImage` VARCHAR NULL , `deskImage` VARCHAR NULL ,
                            PRIMARY KEY (`id`)     );";
                        using (var statement = App.conn.Prepare(sql))
                        {
                            statement.Step();
                        }
                    }
                }
         //done
        public static void insertData(WorkSession wk)
        {
            try
            {
                using (var connection = new SQLiteConnection("Storage.db"))
                {
                    //done

                    using (var statement = connection.Prepare(@"INSERT INTO WorkSessions 
            (StartWorkTime,EndWorkTime,DurationInMinutes,TimeLeftInMinutes,percentageOfWork  ,Title , Description,Stressed ,Tired ,Comfortable ,Noise,OnDesk ,overallState ,stressImage , stressImage,tiredImage , comforImage ,noiseImage ,deskImage)
                                            VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);"))
                    {
                        statement.Bind(1, wk.StartWorkTime);
                        statement.Bind(2, wk.EndWorkTime);
                        statement.Bind(3, wk.DurationInMinutes);
                        statement.Bind(4, wk.TimeLeftInMinutes);
                        statement.Bind(5, wk.percentageOfWork);
                        statement.Bind(6, wk.Title);
                        statement.Bind(7, wk.Description);

                        statement.Bind(8, wk.Stressed);
                        statement.Bind(9, wk.Tired);
                        statement.Bind(10, wk.Comfortable);
                        statement.Bind(11, wk.Noise);
                        statement.Bind(12, wk.OnDesk);
                        statement.Bind(13, wk.overallState);
                        
                        statement.Bind(14, wk.stressImage);
                        statement.Bind(15, wk.tiredImage);
                        statement.Bind(16, wk.comforImage);
                        statement.Bind(17, wk.noiseImage);
                        statement.Bind(18, wk.deskImage);

                        // Inserts data.
                        statement.Step();


                        statement.Reset();
                        statement.ClearBindings();


                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception\n" + ex.ToString());
            }
        }
        //DONE
        public static List<WorkSession> getValues()
        {
            List<WorkSession> list = new List<WorkSession>();

            using (var connection = new SQLiteConnection("Storage.db"))
            {
                using (var statement = connection.Prepare(@"SELECT * FROM WorkSessions ORDER BY id DESC ;"))
                {

                    while (statement.Step() == SQLiteResult.ROW)
                    {

                        list.Add(new WorkSession()
                        {
                            Id = (int)statement[0],
                            StartWorkTime = (DateTime)statement[1],
                            EndWorkTime = (DateTime)statement[2],
                            DurationInMinutes= (int) statement[3],
                            TimeLeftInMinutes= (int)statement[4],
                            percentageOfWork= (int)statement[5],
                            Title= (String) statement[6],
                            Description= (String)statement[7],

                            Stressed= (bool) statement[8],
                            Tired = (bool)statement[9],
                            Comfortable = (bool)statement[10],
                            Noise = (bool)statement[11],
                            OnDesk = (bool) statement[12],
                            overallState = (int) statement[13],

                            stressImage = (String)statement[14],
                            tiredImage = (String)statement[15],
                            comforImage = (String)statement[1],
                            noiseImage = (String)statement[17],
                            deskImage= (String)statement[18],






                        });

                       // Debug.WriteLine(statement[0] + " ---" + statement[1] + " ---" + statement[2]);
                    }
                }
            }
            return list;
        }

        public static void dropTable()
        {
            using (var connection = new SQLiteConnection("Storage.db"))
            {
                using (var statement = connection.Prepare(@"DROP TABLE WorkSessions"))
                {
                    statement.Step();
                }
            }
        }
        //done
        public static void delete(int id)
        {
            using (var connection = new SQLiteConnection("Storage.db"))
            {
                using (var statement = connection.Prepare(@"DELETE FROM WorkSessions WHERE Id=?"))
                {

                    statement.Bind(1, id);
                    statement.Step();
                }
                //  getValues();
            }
        }
        //------------------------------------------------------------
    **/

    }
}

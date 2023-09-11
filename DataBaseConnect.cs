using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace myChat
{
    internal class DBConnect
    {
        string my_query;
        SQLiteCommand myQuery;
        SQLiteConnection myConnection;
        SQLiteDataReader _dr;
        string DBName;
        public DBConnect(string _DBName)
        {
            my_query = @"CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                  @"Password INTEGER(50) UNIQUE NOT NULL ," +
                                  @"UserName TEXT(1, 50)," +
                                  @"LoginName TEXT(1, 50) UNIQUE NOT NULL," +
                                  @"position INTEGER(2));";
            DBName = _DBName;
            sqlConnect(my_query);
            my_query = @"CREATE TABLE IF NOT EXISTS Chats (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                            "name TEXT UNIQUE);";
            sqlConnect(my_query);
            my_query = @"CREATE TABLE IF NOT EXISTS messeges (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                                    @" user_id INTEGER REFERENCES Users (id),"+
                                                     @"chat_id INTEGER REFERENCES Chats (id),"+
                                                     @"messege TEXT,"+
                                                        @" time TEXT);";
            sqlConnect(my_query);

        }
        private void sqlConnect(string _my_query)
        {
            string _source = "Data Source=" + DBName + ".db;";
            string _cache = "Cache=Shared;";
            string _mode = "Mode=ReadWriteCreate;";
            myConnection = new SQLiteConnection(_source + _cache + _mode);
            myConnection.Open();
            myQuery = new SQLiteCommand(_my_query, myConnection);
            _dr = myQuery.ExecuteReader();

        }
        public void AddUser(User name)
        {
            my_query = @"INSERT INTO Users(Password, LoginName) " +
                @"VALUES('" + name.Password + @"','" + name.LoginName + @"');";
            sqlConnect(my_query);
        }
        private void messege(string messege,int user_id,int chat_id) {
            my_query = @"INSERT INTO messeges(user_id, chat_id,messege, time) " +
                @"VALUES(" + user_id + @"," + chat_id + @",'" + messege + @"','" + DateTime.Now.ToString("HH.mm.ss") + @"');";
            sqlConnect(my_query);
        }
        private void updateAdministration(User user, int isAdmin) {
            int position;
            if(user.isAdministration)
                position = 1;
            else position = 0;
            my_query = @"UPDATE Users" +
                        @"SET position = " + position +
                        @"WHERE LoginName = '"+user.LoginName+ "' AND Password = "+user.Password +";";
            sqlConnect(my_query);
        }
        private void updateUserName(User user)
        {            
            my_query = @"UPDATE Users" +
                        @"SET UserName = '" + user.Name+ "'"+
                        @"WHERE LoginName = '" + user.LoginName + "' AND Password = " + user.Password + ";";
            sqlConnect(my_query);
        }
       public List<User> mychatLoad()
        {
            List<User> list = new List<User>();
            User user = new User();
            my_query = @"Select * From Users;";
            sqlConnect(my_query);
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    user.Id = _dr.GetInt32(0);
                    user.Password = _dr.GetInt32(1);
                    user.Name = _dr.GetString(2);
                    user.LoginName = _dr.GetString(3);
                    if (_dr.GetInt32(4) == 0)
                        user.isAdministration = false;
                    else
                        user.isAdministration = true;
                    list.Add(user);
                }
            }
            return list;
        }
        private void AddUserID(User user)
        {
            my_query = @"Select LoginName,Password FROM Users" +
                        @"WHERE LoginName = '" + user.LoginName + "' AND Password = " + user.Password + ";";
            sqlConnect(my_query);
            if (_dr.HasRows)
            {
                while (_dr.Read())
                {
                    var id = _dr.GetInt32(0);
                    user.Id = id;
                }
            }
        }

    }
}

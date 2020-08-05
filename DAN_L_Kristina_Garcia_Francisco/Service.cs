using DAN_L_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_L_Kristina_Garcia_Francisco
{    
    /// <summary>
     /// Class that includes all CRUD functions of the application
     /// </summary>
    class Service
    {
        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Searches for a user with the given username, returns 0 if it does not exist
        /// </summary>
        /// <param name="username">Checks if the user with that Username exists</param>
        /// <returns>The found user id</returns>
        public int FindUserByUsername(string username)
        {
            for (int i = 0; i < GetAllUsers().Count; i++)
            {
                if (GetAllUsers()[i].Username == username)
                {
                    return GetAllUsers()[i].UserID;
                }
            }
            return 0;
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="user">the user that is being added</param> 
        /// <returns>a new user</returns>
        public tblUser AddUser(tblUser user)
        {
            try
            {
                using (AudioDBEntities context = new AudioDBEntities())
                {
                    if (FindUserByUsername(user.Username) == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            Username = user.Username,
                            UserPassword = user.UserPassword
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return newUser;
                    }
                    else
                    {
                        user.UserID = FindUserByUsername(user.Username);
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}

using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsUserJoiningApplication
    {
        public enum enMode { AddNew, Update };
        enMode _Mode = enMode.AddNew;

        public enum enStatus { New = 0, Deleted = 1, Completed = 2 };

        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;

        public int Status { get; set; }
        public DateTime ApplicationDate { get; set; }

        public clsUserJoiningApplication() 
        {
            _Mode = enMode.AddNew;
            ApplicationID = -1; PersonID = -1; Status = (int)enStatus.New;
            ApplicationDate = DateTime.Now;
        }

        public clsUserJoiningApplication(int ApplicationID, int PersonID
            , int Status, DateTime ApplicationDate)
        {
            _Mode = enMode.Update;
            this.ApplicationID = ApplicationID; this.PersonID = PersonID;
            this.Status = Status; this.ApplicationDate = ApplicationDate;
        }

        public static clsUserJoiningApplication FindByID(int ApplicationID) 
        {
            int personID = -1; int status = -1; DateTime applicationDate = DateTime.Now;
            bool IsFound = clsUserJoiningApplicationData.GetApplicationByID(ApplicationID, ref personID
                , ref status, ref applicationDate);

            if (IsFound) return new clsUserJoiningApplication
                    (ApplicationID, personID, status, applicationDate);
            else return null;
        }

        public static clsUserJoiningApplication FindByPersonID(int PersonID)
        {
            int applicationID = -1; int status = -1; DateTime applicationDate = DateTime.Now;
            bool IsFound = clsUserJoiningApplicationData.GetApplicationByPersonID(PersonID
                , ref applicationID, ref status, ref applicationDate);

            if (IsFound) return new clsUserJoiningApplication
                    (applicationID, PersonID, status, applicationDate);
            else return null;
        }

        private bool _AddNewApplication() 
        {
            this.ApplicationID = clsUserJoiningApplicationData
                .AddNewApplication(PersonID, Status, ApplicationDate);
            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsUserJoiningApplicationData.UpdateApplication
                (ApplicationID, PersonID, Status, ApplicationDate);
        }

        public bool Save() 
        {
            switch(_Mode) 
            {
                case enMode.AddNew: 
                    {
                        if (_AddNewApplication()) 
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                case enMode.Update: 
                    {
                        return _UpdateApplication();
                    }
            }return false;
        }

        public static DataTable GetAllApplications() 
        {
            return clsUserJoiningApplicationData.GetAllApplications();
        }

        public static clsUserJoiningApplication UpdateStatus(int ApplicationID, int Status) 
        {
            clsUserJoiningApplication Application = clsUserJoiningApplication.FindByID(ApplicationID);
            if (Application == null) return null;

            Application.Status = Status;
            Application.ApplicationDate = DateTime.Now;
            if (!Application.Save()) return null;
            return Application;
        }

        public clsUserJoiningApplication UpdateStatus(int Status)
        {
            clsUserJoiningApplication Application = clsUserJoiningApplication.FindByID(ApplicationID);
            if (Application == null) return null;

            Application.Status = Status;
            Application.ApplicationDate = DateTime.Now;
            if (!Application.Save()) return null;
            return Application;
        }

        public clsUserJoiningApplication CompleteApplication() 
        {
            return clsUserJoiningApplication.UpdateStatus(ApplicationID, 2);
        }

        public static clsUserJoiningApplication CompleteApplication(int ApplicationID)
        {
            return clsUserJoiningApplication.UpdateStatus(ApplicationID, 2);
        }

        public clsUserJoiningApplication CancelApplication()
        {
            return clsUserJoiningApplication.UpdateStatus(ApplicationID, 1);
        }

        public static clsUserJoiningApplication CancelApplication(int ApplicationID)
        {
            return clsUserJoiningApplication.UpdateStatus(ApplicationID, 1);
        }

    }
}

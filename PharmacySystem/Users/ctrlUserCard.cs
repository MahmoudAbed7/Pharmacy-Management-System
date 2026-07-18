using PharmacyBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Users
{
    public partial class ctrlUserCard : UserControl
    {
        int _UserID = -1;
        clsUser _User;

        public int UserID { get { return _UserID; } }
        public clsUser UserInfo {  get { return _User; } }

        public void LoadUserData(int UserID) 
        {
            _UserID = UserID;
            _User = clsUser.FindByID(UserID);
            if( _User == null) 
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with id = " + UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _ResetPersonInfo() 
        {
            ctrlPersonCard1.resetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
        private void _FillUserInfo() 
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            _UserID = _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        
    }
}

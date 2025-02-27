﻿using MessageServerClient;
using System;
using System.Windows.Forms;

namespace MessageServerClientDemo
{
    public partial class FrmSms : Form
    {
        string smsJwt = "";
        DateTime smsJwtexpirty;
        DateTime smsSubscriptionExpiry;

        public FrmSms()
        {
            InitializeComponent();
        }

        async private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            var client = new SmsClient();
            var ar = await client.Authenticate(txtUserName.Text, txtPassword.Text);

            if (ar != null)
            {
                smsJwt = ar.Jwt;
                smsSubscriptionExpiry = ar.SubscriptionExpiryDate;
                smsJwtexpirty = ar.JwtExpiry;
                var expirtystr = $"Token valid Until {ar.JwtExpiry}\n\rSubscription valid until {ar.SubscriptionExpiryDate}";
                lblExpiry.Text = expirtystr;
            }
            else
            {
                MessageBox.Show(this, "authentication Failed");
            }
        }

        async private void btnSendText_Click(object sender, EventArgs e)
        {
            var client = new SmsClient(smsJwt);
            if (await client.SendTextAsync(txtPhoneNo.Text, txtMessage.Text))
            {
                MessageBox.Show("suceeded");
            }
            else
            {
                MessageBox.Show("failed");
            }
            
        }
    }
}

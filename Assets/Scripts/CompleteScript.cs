using System;
using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Parameters;
using UnityEngine;
using UnityEngine.UI;
using Algorand;
using Algorand.V2;
using Algorand.Client;
using Algorand.V2.Model;
using Account = Algorand.Account;

public class CompleteScript : MonoBehaviour
{   
    //This script is the controller for my entire wallet demo. It combines all of the
    //different functions outlined in the other specific scripts but links them to the
    //UI elements.
    
    public Text addressText;
    public Text totalText;

    
    public GameObject connectUI;
    public GameObject mnemonicField;

    public GameObject sendUI;
    public GameObject amountField;
    public GameObject recipientField;

    public string ALGOD_API_ADDR = "your algod api address"; //find in algod.net
    public string ALGOD_API_TOKEN = "your algod api token"; //find in algod.token
    public string SRC_ACCOUNT;
    private string totalAlgos;
    public AlgodApi algodApiInstance;
    
    public long lastRound;
    public long firstRound;
    public string toAddress;
    public float sendAmount;
    public string sendAmountString;

    public Account myAccount;

    void Start()
    {  
        AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);
    
        try
        {
            var supply = algodApiInstance.GetSupply();
            Debug.Log("Total Algorand Supply: " + supply.TotalMoney);
            Debug.Log("Online Algorand Supply: " + supply.OnlineMoney);
        }

        catch (ApiException e)
        {
            Debug.Log("Exception when calling algod#getSupply: " + e.Message);
        }

        Refresh();
    }

    void Update()
    {

    }

    public void Generate() 
    {
        myAccount = new Account();
        Debug.Log("My Address: " + myAccount.Address);
        Debug.Log("My Passphrase: " + myAccount.ToMnemonic());
        SRC_ACCOUNT = myAccount.ToMnemonic();
        Total();
    }

    public void Total()
    {
        Account myAccount = new Account(SRC_ACCOUNT);
        Debug.Log("Connected to:" + myAccount.Address.ToString());
        addressText.text = ("Connected To: " + myAccount.Address.ToString());

        AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);            

        var accountInfo = algodApiInstance.AccountInformation(myAccount.Address.ToString());
        totalAlgos = (string.Format("{0:0.000000}", accountInfo.Amount/1000000.000000));

        if (String.IsNullOrEmpty(totalAlgos) || totalAlgos == "0") 
        {
            totalText.text = "0.000000";
        }

        else
        {
            totalText.text = totalAlgos;
        }
    }

    public void Refresh()
    {
        Total();
        
    }

    public void ConnectWalletButton()
    {
        connectUI.SetActive (true);
    }
    
    public void Connect()
    {
        SRC_ACCOUNT = mnemonicField.GetComponent<Text>().text;
        connectUI.SetActive (false);
        Refresh();
    }

    public void CloseConnect()
    {
        connectUI.SetActive (false);
    }

    public void SendButton()
    {
        sendUI.SetActive (true);
    }
    
    public void Send()
    {
        Account myAccount = new Account(SRC_ACCOUNT);
        AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);
        toAddress = recipientField.GetComponent<Text>().text;
        sendAmountString = amountField.GetComponent<Text>().text;
        sendAmount= float.Parse(sendAmountString);
        sendAmount *= 1000000;
        var block = algodApiInstance.GetBlock((long?)firstRound);
        lastRound = firstRound + 1;
        Debug.Log ("Last Block: " + firstRound);
        Debug.Log ("To Address: " + toAddress);
        Debug.Log ("From Address: " + myAccount.Address);
        var tx = new Algorand.Transaction(myAccount.Address, 1000, (ulong)firstRound, (ulong)lastRound, null, (ulong)sendAmount, new Address(toAddress), null, new Digest());
        SignedTransaction signedTx = myAccount.SignTransaction(tx);
        byte[] signedTxBytes = Encoder.EncodeToMsgPack(signedTx);
        string signedTxHex = Encoder.EncodeToHexStr(signedTxBytes);
        string txID = signedTx.transactionID;
        sendUI.SetActive (false);
        Refresh();
    }

    public void CloseSend()
    {
        sendUI.SetActive (false);
    }
}
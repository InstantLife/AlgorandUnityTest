using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Algorand;
using Algorand.V2;
using Algorand.Client;
using Algorand.V2.Model;
using Account = Algorand.Account;


public class GetWalletAmount : MonoBehaviour
{
    void Start()
    {
        string ALGOD_API_ADDR = "https://testnet-algorand.api.purestake.io/ps2";
        string ALGOD_API_TOKEN = "DRXnAEFg5v8MN1Paeh5kk5S62PWPIVrY6zhzgCI8";
        string SRC_ACCOUNT = "typical permit hurdle hat song detail cattle merge oxygen crowd arctic cargo smooth fly rice vacuum lounge yard frown predict west wife latin absent cup";

        Account src = new Account(SRC_ACCOUNT);
        Debug.Log("My account address is:" + src.Address.ToString());

        AlgodApi algodApiInstance = new AlgodApi(ALGOD_API_ADDR, ALGOD_API_TOKEN);            

        var accountInfo = algodApiInstance.AccountInformation(src.Address.ToString());
        Debug.Log(string.Format("Account Balance: {0} microAlgos", accountInfo.Amount));
        }
}
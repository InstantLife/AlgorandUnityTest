using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand;
using Algorand.V2;
using Algorand.Client;
using Algorand.V2.Model;
using Account = Algorand.Account;

public class ConnectToNode : MonoBehaviour
{   
    public string ALGOD_API_ADDR = "your algod api address"; //find in algod.net
    public string ALGOD_API_TOKEN = "your algod api token"; //find in algod.token

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand;
using Algorand.V2;
using Algorand.Client;
using Algorand.V2.Model;
using Account = Algorand.Account;

public class GenerateWallet : MonoBehaviour
{   
    public void Main() 
    {
        Account myAccount = new Account();
        Debug.Log("My Address: " + myAccount.Address);
        Debug.Log("My Passphrase: " + myAccount.ToMnemonic());
    }
}

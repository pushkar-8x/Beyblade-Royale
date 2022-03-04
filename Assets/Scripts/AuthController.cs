using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
public class AuthController : MonoBehaviour
{
    public TextMeshProUGUI statusText;
   
    public TMP_InputField emailText, passwordText;


   

    public void LoginUser()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailText.text, passwordText.text).ContinueWith((task =>
        {

            if(task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);

                return;
            }

            if(task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);

               

                return;
            }

            if(task.IsCompleted)
            {


                LoginDone();

            }



        }));
    }

    public void RegisterUser()
    {

       

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailText.text, passwordText.text).ContinueWith((task =>
        {

            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);

                return;
            }

            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                
                return;
            }

            if (task.IsCompleted)
            {
                // Debug.Log("Registration Success !");

                RegistrationDone();
                
            }



        }));
    }


    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        statusText.text = msg;
        Debug.Log(msg);
       

    }

    void RegistrationDone()
    {
        statusText.text = "Registration Successful , Login to continue !";
        
    }

    void LoginDone()
    {
        
        statusText.text = " Logging in ..";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


   

    

    
}

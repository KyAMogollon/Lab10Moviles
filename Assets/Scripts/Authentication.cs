using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;
using TMPro;

public class Authentication : MonoBehaviour
{
    [SerializeField] private string email;
    [SerializeField] private string password;
    [Header ("SignIn")]
    [SerializeField] TextMeshProUGUI SignInEmail;
    [SerializeField] TextMeshProUGUI SignInPassword;
    [Header("SignUp")]
    [SerializeField] TextMeshProUGUI SignUpEmail;
    [SerializeField] TextMeshProUGUI SignUpPassword;
    [Header("Data")]
    [SerializeField] SaveDataSO saveData;
    private FirebaseAuth _authReference;

    public UnityEvent OnLogInSuccesful = new UnityEvent();

    private void Awake()
    {
        _authReference = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
    }

    public void SignUp()
    {
        Debug.Log("Start Register");
        StartCoroutine(RegisterUser(email, password));
    }

    public void SignIn()
    {
        Debug.Log("Start Login");
        StartCoroutine(SignInWithEmail(email, password));
    }

    public void SignOut()
    {
        LogOut();
    }

    public void LoadNewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator RegisterUser(string email, string password)
    {
        Debug.Log("Registering");
        email = SignUpEmail.text;
        password = SignUpPassword.text;
        var registerTask = _authReference.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
        }
        else
        {
            Debug.Log($"Succesfully registered user {registerTask.Result.User.Email}");
        }
    }

    private IEnumerator SignInWithEmail(string email, string password)
    {
        Debug.Log("Loggin In");
        email = SignInEmail.text;
        password = SignInPassword.text;
        var loginTask = _authReference.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogWarning($"Login failed with {loginTask.Exception}");
        }
        else
        {
            Debug.Log($"Login succeeded with {loginTask.Result.User.Email}");
            saveData.SetEmail(email);
            OnLogInSuccesful?.Invoke();
        }
    }

    private void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }

}

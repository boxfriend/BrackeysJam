using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Replace DeveloperName with your own name within the project
/// Add a .PackageName (named however you prefer) if you want to organize your code even further
/// Example would be: namespace Boxfriend.Player
/// For More organization, organize packages into similarly named folders
/// </summary>
namespace DeveloperName
{
    /// <summary>
    /// Make sure to add a summary of what the class does
    /// </summary>
    public class ScriptGuideline : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// Public fields should have a summary if possible or not immediately obvious what they are for
        /// </summary>
        [Tooltip("Public variables should have a tooltip description for use in inspector")]
        public string examplePublicString;


        [SerializeField, Range(0, 1), Tooltip("Serialized fields should always have tooltip. Numeric variables should have a range if necessary")]
        private int _examplePrivateInt;
        #endregion

        #region Properties
        /// <summary>
        /// Properties will have PascalCase names that match their private variables
        /// </summary>
        public int ExamplePrivateInt { get; private set; }
        #endregion

        #region MonoBehaviours
        //Feel free to label your monobehaviours or even add a summary, but this is not necessary
        void Awake()
        {
            //Make sure to leave comments if it is not immediately obvious what a line of code does
            var example = examplePublicString;

            if (true)
            {
                //If statements should always use curly brackets even if it only executes a single line
            }
        }

        void Start()
        {

        }

        /// <summary>
        /// Do not do ANY physics or movement calculations in Update
        /// Use Update only for anything that does not require a fixed time step
        /// </summary>
        void Update()
        {
            
        }

        /// <summary>
        /// Fixed Update should be used for Physics and movement.
        /// It may also be used for anything that requires a fixed time step rather than variable timestep offered by Update()
        /// </summary>
        void FixedUpdate()
        {

        }

        void OnCollisionEnter2D(Collision2D collision)
        {

        }
        #endregion

        #region Input
        // Events based on actions from an Input Action Map that you are using
        
        /// <summary>
        /// These methods are called automatically from a PlayerInput (or other Input) component
        /// when the relevant button/input has been activated.
        /// Called once on press and once on release
        /// </summary>
        void OnMove()
        {

        }

        void OnJump()
        {

        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// All public methods should have a summary
        /// </summary>
        public void DoAThing()
        {

        }

        /// <summary>
        /// This is an example of a return type method
        /// </summary>
        /// <returns>Summarize what the method returns</returns>
        public int GetPlayerHealth()
        {
            return 1;
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Events should start with the prefix On
        /// </summary>
        void OnPlayerDeath()
        {

        }

        /// <summary>
        /// Boolean methods should be named similar to boolean variables.
        /// see project guidelines
        /// </summary>
        /// <returns>Summarize what the method returns</returns>
        bool IsDead()
        {
            return true;
        }
        #endregion
    }
}

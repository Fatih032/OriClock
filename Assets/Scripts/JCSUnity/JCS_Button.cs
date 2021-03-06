/**
 * $File: JCS_Button.cs $
 * $Date: $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *                   Copyright (c) 2016 by Shen, Jen-Chieh $
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace JCSUnity
{

    /// <summary>
    /// Buttton Interface (NGUI)
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public abstract class JCS_Button
        : MonoBehaviour
    {

        /*******************************************/
        /*            Public Variables             */
        /*******************************************/
        public delegate void CallBackFunc();
        public delegate void CallBackFuncBtn(JCS_Button btn);
        public delegate void CallBackFuncBtnInt(int selection);

        // JCSUnity framework only callback, do not override this callback.
        public CallBackFunc btnSystemCallBack = null;
        public CallBackFuncBtn btnSystemCallBackBtn = null;
        public CallBackFuncBtnInt btnSystemCallBackBtnInt = null;
        // for user's callback.
        public CallBackFunc btnCallBack = null;
        public CallBackFuncBtn btnCallBackBtn = null;

        public CallBackFunc interactableCallback = null;

        /*******************************************/
        /*           Private Variables             */
        /*******************************************/

        [Header("** Check Variables (JCS_Button) **")]

        [Tooltip("Record down the selection choice for dialogue system.")]
        [SerializeField]
        private int mDialogueSelection = -1;

        private bool mInitialized = false;

        /*******************************************/
        /*           Protected Variables           */
        /*******************************************/

        [Header("** Optional Variables (JCS_Button) **")]

        [Tooltip("text under the button, no necessary.")]
        [SerializeField]
        protected Text mButtonText = null;


        [Header("** Initialize Variables (JCS_Button) **")]

        [Tooltip("Auto add listner to button click event?")]
        [SerializeField] protected bool mAutoListener = true;
        [Tooltip("Index pairing with Dialogue, in order to call the correct index.")]
        [SerializeField] protected int mDialogueIndex = -1;


        [Header("** Runtime Variables (JCS_Button) **")]

        [Tooltip("Is the button interactable or not. (Default: true)")]
        [SerializeField]
        protected bool mInteractable = true;

        [Tooltip("Color tint when button is interactable.")]
        [SerializeField]
        protected Color mInteractColor = new Color(1, 1, 1, 1);

        [Tooltip("Color tint when button is not interactable.")]
        [SerializeField]
        protected Color mNotInteractColor = new Color(1, 1, 1, 0.5f);


        protected RectTransform mRectTransform = null;
        protected Button mButton = null;
        protected Image mImage = null;

        /*******************************************/
        /*             setter / getter             */
        /*******************************************/
        public RectTransform GetRectTransfom() { return this.mRectTransform; }
        public Button ButtonComp { get { return this.mButton; } }
        public Image Image { get { return this.mImage; } }
        public int DialogueIndex { get { return this.mDialogueIndex; } set { this.mDialogueIndex = value; } }
        public bool AutoListener { get { return this.mAutoListener; } set { this.mAutoListener = value; } }
        public bool Interactable {
            get { return this.mInteractable; }
            set
            {
                mInteractable = value;

                // set this, in order to get the effect immdediatly.
                SetInteractable();
            }
        }
        public Text ButtonText { get { return this.mButtonText; } }
        /* Compatible with 1.5.3 version of JCSUnity */
        public void SetCallback(CallBackFunc func) { this.btnCallBack += func; }
        public void SetCallback(CallBackFuncBtn func) { this.btnCallBackBtn += func; }
        public void SetSystemCallback(CallBackFunc func) { this.btnSystemCallBack += func; }
        public void SetSystemCallback(CallBackFuncBtn func) { this.btnSystemCallBackBtn += func; }
        public void SetSystemCallback(CallBackFuncBtnInt func, int selection)
        {
            this.btnSystemCallBackBtnInt += func;
            this.mDialogueSelection = selection;
        }

        public int DialogueSelection { get { return this.mDialogueSelection; } }

        /*******************************************/
        /*            Unity's function             */
        /*******************************************/
        protected virtual void Awake()
        {
            InitJCSButton();
        }

        /*******************************************/
        /*              Self-Define                */
        /*******************************************/
        //----------------------
        // Public Functions

        /// <summary>
        /// Intialize jcs button once.
        /// </summary>
        public void InitJCSButton(bool forceInit = false)
        {
            if (!forceInit)
            {
                if (this.mInitialized)
                    return;
            }

            this.mRectTransform = this.GetComponent<RectTransform>();
            this.mButton = this.GetComponent<Button>();
            this.mImage = this.GetComponent<Image>();

            // try to get the text from the child.
            if (mButtonText == null)
                mButtonText = this.GetComponentInChildren<Text>();

            if (mAutoListener)
            {
                // add listener itself, but it won't show in the inspector.
                mButton.onClick.AddListener(JCS_ButtonClick);
            }

            // set the stating interactable.
            SetInteractable();

            // part of the on click callback.
            SetSystemCallback(JCS_OnClickCallback);

            this.mInitialized = true;
        }

        /// <summary>
        /// Default function to call this, so we dont have to
        /// search the function depends on name.
        /// 
        /// * Good for organize code and game data file in Unity.
        /// </summary>
        public virtual void JCS_ButtonClick()
        {
            if (!mInteractable)
                return;

            /* System callback */
            if (btnSystemCallBack != null)
                btnSystemCallBack.Invoke();

            if (btnSystemCallBackBtn != null)
                btnSystemCallBackBtn.Invoke(this);

            if (btnSystemCallBackBtnInt != null)
                btnSystemCallBackBtnInt.Invoke(this.mDialogueSelection);

            /* User callback */
            if (btnCallBack != null)
                btnCallBack.Invoke();

            if (btnCallBackBtn != null)
                btnCallBackBtn.Invoke(this);
        }

        /// <summary>
        /// This is the callback when the button get click.
        /// </summary>
        public abstract void JCS_OnClickCallback();

        /// <summary>
        /// Use this to enable and disable the button.
        /// </summary>
        /// <param name="act"></param>
        public virtual void SetInteractable(bool act, bool fromSelection)
        {
            mInteractable = act;

            /* Make sure no error! */
            {
                if (mButton == null)
                    InitJCSButton();
            }
            mButton.enabled = mInteractable;

            if (mInteractable)
            {
                mImage.color = mInteractColor;
            }
            else
            {
                mImage.color = mNotInteractColor;
            }

            // interactable callback.
            if (interactableCallback != null)
                interactableCallback.Invoke();
        }
        public virtual void SetInteractable(bool act)
        {
            SetInteractable(act, false);
        }
        public virtual void SetInteractable()
        {
            SetInteractable(mInteractable, false);
        }

        //----------------------
        // Protected Functions

        //----------------------
        // Private Functions

    }
}

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class AnimController : MonoBehaviour
//{

//    [SerializeField] private Animator _animator;

//    public Animator animator
//    {
//        get { return _animator; }
//    }

//    private void Start()
//    {
//        _animator = GetComponent<Animator>();


//        if (_animator == null)
//            _animator = GetComponent<Animator>();

//        if (_animator == null)
//            Debug.LogError("Animator not found on AnimController.");


//    }

//    public void PlayAnim(string animName, float duration)
//    {
//        //Debug.Log($"Play Anim: {animName}");


//    }
//    //public Animator _animator;

//    //public Animator animator
//    //{
//    //    get { return _animator; }
//    //}

//    //private void Start()
//    //{
//    //    _animator = GetComponent<Animator>();

//    //    //if (_animator == null )
//    //    //{
//    //    //    Debug.Log("Anim is null");
//    //    //}
//    //}

//    //public void PlayAnim(string animName, float duration)
//    //{
//    //    //Debug.Log($"Play Anim: {animName}");
//    //    _animator.CrossFade(animName, duration);
//    //}
//}


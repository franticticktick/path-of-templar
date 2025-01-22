using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterComponent<T> : MonoBehaviour, ICharacterComponent where T : Character
{
    [SerializeField]
    protected T character;

    [SerializeField]
    private Transform[] bodyElements;

    [SerializeField]
    private List<NamedAudioClip> nameAudioClips;

    protected AudioSource audioSource;

    protected CharacterNavigator characterNavigator;

    private HitEffect hitEffect;

    protected Action action;

    protected Vector3 movingTarget;

    protected Transform interactionTarget;

    protected CharacterAnimator characterAnimator;

    protected CapsuleCollider capsuleCollider;

    [SerializeField]
    private string GROUND_NAME = "Ground";

    void Start()
    {
        Init();
    }

    void Update()
    {
        action?.Invoke();
        RunBehavior();
    }

    protected virtual void Init()
    {
        characterAnimator = new CharacterAnimator(GetComponent<Animator>());
        hitEffect = GetComponent<HitEffect>();
        characterNavigator = CharacterNavigator(GetComponent<NavMeshAgent>());
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        DisableAction();

        capsuleCollider = GetComponent<CapsuleCollider>();
        hitEffect = GetComponentInChildren<HitEffect>(true);

        characterNavigator.OnStopMove += OnMovementStop;
        characterNavigator.OnStopAttack += OnStopAttack;
        characterNavigator.OnStartAttack += OnStartAttack;
        characterNavigator.OnStartComingUp += OnStartInteract;

        character.OnDeath += OnDeath;
    }

    public void PlayFootStepAudioClip()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
        {
            if (hit.transform.GetComponent<Terrain>() != null)
            {
                Terrain terrain = hit.transform.GetComponent<Terrain>();
                var layerName = TerrainChecker.GetLayerName(transform.position, terrain);

                if (nameAudioClips != null)
                {
                    FindAndPlayAudioClip(layerName);
                }
            }

            if (hit.transform.GetComponent<MeshCollider>() != null)
            {
                FindAndPlayAudioClip(GROUND_NAME);
            }
        }
    }

    private void FindAndPlayAudioClip(string tag)
    {
        var nameAudioClip = nameAudioClips.Find(clip => clip.name == tag);
        var footStepAudioClip = nameAudioClip.RandomAudioClip();

        PlayAudioClip(footStepAudioClip, nameAudioClip.volume);
    }

    private void PlayAudioClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    protected abstract void RunBehavior();

    protected virtual CharacterNavigator CharacterNavigator(NavMeshAgent agent)
    {
        return new CharacterNavigator(agent);
    }

    public virtual void Damage()
    {
        MakeDamage(character.Damage());
    }

    protected void MakeDamage(Hit hit)
    {
        if (interactionTarget != null)
        {
            var characterEngineFacade = interactionTarget.GetComponent<ICharacterComponent>();
            var dead = characterEngineFacade.ReceiveDamage(hit, transform);

            if (dead)
            {
                StopAttack();
                DisableAction();
            }
        }
    }

    private void StopAttack()
    {
        characterAnimator.DisableAnimations();
    }

    public virtual bool ReceiveDamage(Hit hit, Transform target)
    {
        var namedAudioClip = nameAudioClips.Find(clip => clip.name == "Damage");
        var damageAudioclip = namedAudioClip.RandomAudioClip();

        PlayAudioClip(damageAudioclip, 1);

        ShowDamageHitEffect();

        return character.ReceiveDamage(hit);
    }

    private void ShowDamageHitEffect()
    {
        GameObject newHitEffect = Instantiate(hitEffect.gameObject, hitEffect.transform.position, Quaternion.identity);
        newHitEffect.SetActive(true);

        Destroy(newHitEffect, 2f);
    }

    public void Move()
    {
        characterNavigator.Move(transform.position, movingTarget);
    }

    public void Attack()
    {
        characterNavigator.Attack(transform, interactionTarget.position);
    }

    protected virtual void MoveToTarget(Vector3 movingTarget)
    {
        this.movingTarget = movingTarget;
        interactionTarget = null;

        action = Move;
    }

    protected virtual void AttackTarget(Transform interactionTarget)
    {
        this.interactionTarget = interactionTarget;
        action = Attack;
    }

    protected void OnMovementStop(bool stop)
    {
        if (stop)
        {
            DisableAction();
            characterAnimator.DisableAnimations();
        }
    }

    protected void OnStartAttack(bool start)
    {
        if (start)
        {
            characterAnimator.StartAttackAnimation();
        }
    }

    protected void OnStartInteract(bool start)
    {
        if (start)
        {
            characterAnimator.StartWalkAnimation();
        }
    }

    protected void OnStopAttack(bool stop)
    {
        if (stop)
        {
            characterAnimator.StartWalkAnimation();
        }
    }

    protected virtual void OnDeath(bool dead)
    {
        if (dead)
        {
            DisableAction();
            characterAnimator.DisableAnimationsWithAnimator();

            capsuleCollider.enabled = false;

            DisableKinematicForBody();
        }
    }

    private void DisableKinematicForBody()
    {
        foreach (Transform body in bodyElements)
        {
            body.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    protected void DisableAction()
    {
        action = () => { };
    }

    public Character Character()
    {
        return character;
    }
}

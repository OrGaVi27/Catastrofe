using UnityEngine;
using UnityEngine.UI;

public partial class BossBehaviour
{
    [Header("Boss")]
    public Animator bossAnim;
    [Space]

    [Header("Elements")]
    public bool fire;
    public bool water;
    public bool electricity;
    public bool rock;

    public int element;

    public Material[] materials;

    public GameObject[] vfx;

    private Color rojoPersonalizado;
    private Color marronPersonalizado;
    private Color azulPersonalizado;
    private Color amarilloPersonalizado;

    public Image Vida;
    public Image Vida2;

    /*public List<string> activeElements = new List<string>();
    public List<string> allElements = new List<string>();*/
    [Space]

    [Header("Life")]
    public int lifesCount;
    public Slider lifeSlider;

    public Image lifeColor;

    public float maxLiife;
    public float currentLiife;
    [Space]

    [Header("Pelea")]
    public float timeAttack;
    private float currentTimeAttack;
    public MeleeAtack melee;
    public DistanceAttack distance;

    private bool inAttack;

    public BossMovement move;

    private int MA;
    private int DA;
    [Space]

    [Header("MelleCollider")]
    public GameObject SwordDamage1;
    public GameObject SwordDamage2;
    [Space]

    [Header("PuntosDisparo")]
    public GameObject bola;
    public GameObject preRayo;
    public GameObject rayo;
    [Space]
    public Transform point01;
    public Transform point02;
    public Transform point03;
    public Transform point04;
    public Transform point05;
    public Transform point06;
    public Transform point07;
    public Transform point08;
    [Space]

    [Header("Muerte")]
    public bool deathBoss;
}

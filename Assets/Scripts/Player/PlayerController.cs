using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _playerControls;
    private Vector2 			_playerDirection;
    private Rigidbody2D 		_playerRB2D;
	private Animator 			_playerAnimator;

    [Header("Movimento")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float multiplicadorCorrida = 1.4f;

    private bool _correndo;

    [Header("Interacao")]
    [SerializeField] private BalaoColetaUI balaoColetaUI;

    [Header("Visual")]
    [SerializeField] private Transform visualRoot;

    private readonly List<Item> _itensEmContato = new List<Item>();
    private Item _itemEmFoco;

    private void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
            _playerControls = new InputSystem_Actions();

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerRB2D = GetComponent<Rigidbody2D>();

        if (visualRoot != null)
        {
            _playerAnimator = visualRoot.GetComponent<Animator>();
        }

        if (_playerAnimator == null)
        {
            Debug.LogError("Animator do player nao foi encontrado. Configure o campo Visual Root com o objeto visual que possui o Animator.");
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.OnColetaAtualizada += AtualizarBalaoColeta;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnColetaAtualizada -= AtualizarBalaoColeta;
        }
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        _playerDirection = _playerControls.Player.Move.ReadValue<Vector2>();
        _correndo = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

		if (_playerAnimator != null) {
			if (_playerDirection.sqrMagnitude <= 0) {
				_playerAnimator.SetInteger("Movimento", 0);
			} else if (_correndo) {
				_playerAnimator.SetInteger("Movimento", 2);
			} else {
				_playerAnimator.SetInteger("Movimento", 1);
			}
		}

		Flip();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TentarColetarItemEmFoco();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TentarRemoverItemColetado();
        }
    }

    private void PlayerMove()
    {
        float velocidadeAtual = _correndo ? playerSpeed * multiplicadorCorrida : playerSpeed;

        _playerRB2D.MovePosition(
            _playerRB2D.position + _playerDirection.normalized * velocidadeAtual * Time.fixedDeltaTime
        );
    }

	void Flip()
	{
        Transform alvoFlip = visualRoot != null ? visualRoot : transform;

		if(_playerDirection.x > 0) {
			alvoFlip.eulerAngles = new Vector2(0f, 0f);
		} else if(_playerDirection.x < 0){
			alvoFlip.eulerAngles = new Vector2(0f, 180f);
		}
	}

    public void RegistrarItemProximo(Item item)
    {
        if (item == null || _itensEmContato.Contains(item))
            return;

        _itensEmContato.Add(item);

        if (_itemEmFoco == null)
        {
            DefinirItemEmFoco(item);
        }
    }

    public void RemoverItemProximo(Item item)
    {
        if (item == null)
            return;

        _itensEmContato.Remove(item);

        if (_itemEmFoco != item)
            return;

        _itemEmFoco = null;

        if (_itensEmContato.Count > 0)
        {
            DefinirItemEmFoco(_itensEmContato[0]);
        }
        else if (balaoColetaUI != null)
        {
            balaoColetaUI.Esconder();
        }
    }

    private void DefinirItemEmFoco(Item item)
    {
        _itemEmFoco = item;
        AtualizarBalaoColeta();
    }

    private void TentarColetarItemEmFoco()
    {
        if (_itemEmFoco == null || _itemEmFoco.ItemData == null || GameManager.instance == null)
            return;

        GameManager.instance.AdicionarItem(_itemEmFoco.ItemData);
        AtualizarBalaoColeta();
    }

    private void TentarRemoverItemColetado()
    {
        if (GameManager.instance == null)
            return;

        GameManager.instance.RemoverItem();
        AtualizarBalaoColeta();
    }

    private void AtualizarBalaoColeta()
    {
        if (balaoColetaUI == null)
            return;

        if (_itemEmFoco == null || _itemEmFoco.ItemData == null)
        {
            balaoColetaUI.Esconder();
            return;
        }

        int quantidade = 0;
        if (GameManager.instance != null &&
            GameManager.instance.SaoMesmoItem(GameManager.instance.ItemColetadoAtual, _itemEmFoco.ItemData))
        {
            quantidade = GameManager.instance.ItensColetados;
        }

        balaoColetaUI.Mostrar(_itemEmFoco.ItemData, quantidade);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockPlacer : MonoBehaviour
{
    public bool blockPlacerOn;
    public bool expandingBlock;
    public Toggle placerToggle;
    public InputField screenSize;
    public SpriteRenderer block;
    public LayerMask layerMask;

    SpriteRenderer blockSprite;
    Vector3 origin;
    BoxCollider2D blockCollider;

    public void Start()
    {
        SetValues();
    }

    public void SetValues()
    {
        placerToggle.isOn = blockPlacerOn;
        screenSize.text = Camera.main.orthographicSize + "";
    }

    public void SetCameraSize()
    {
        Camera.main.orthographicSize = float.Parse(screenSize.text);
        try
        {
            Camera.main.orthographicSize = float.Parse(screenSize.text);
        }
        catch { }
    }

    public void Update()
    {
        if (placerToggle.isOn)
        {
            if (expandingBlock)
            {
                ExpandBlockToMouse();
            }
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SpawnBlock();
            }
            if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                blockCollider.size = new Vector2(Mathf.Abs(blockSprite.size.x), Mathf.Abs(blockSprite.size.y));
                expandingBlock = false;
                blockSprite = null;
            }

            if(Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                FindObjectAtMouse();
            }
            if (Input.GetMouseButton(1) && blockSprite != null)
            {
                Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                camPos.z = 0;
                blockSprite.transform.position = camPos;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if(blockSprite != null)
                {
                    Destroy(blockSprite.gameObject);
                    blockSprite = null;
                }
            }
        }
    }

    public void SpawnBlock()
    {
        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        origin.z = 0;
        blockSprite = Instantiate(block);
        blockSprite.transform.position = origin;
        blockCollider = blockSprite.GetComponent<BoxCollider2D>();
        expandingBlock = true;
    }

    public void ExpandBlockToMouse()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0;

        Vector3 size = Vector2.zero;
        size.x = camPos.x - origin.x;
        size.y = camPos.y - origin.y;

        blockSprite.size = size;
        blockSprite.transform.position = origin + size / 2;
    }

    public void FindObjectAtMouse()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0;
        Collider2D hit = Physics2D.OverlapCircle(camPos, 1f, layerMask);
        if (hit)
        {
            blockSprite = hit.GetComponent<SpriteRenderer>();
        }
        else
        {
            blockSprite = null;
        }
    }

    public void ToggleBlockPlacer(bool b)
    {
        blockPlacerOn = b;
    }
}

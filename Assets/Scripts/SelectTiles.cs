using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTiles : MonoBehaviour
{
    [SerializeField] 
    private LayerMask _highlightLayer; // Маска слоя для определения, какие объекты могут быть подсвечены.
    
    private GameObject _lastHighlightedTile; // Последняя подсвеченная плитка.

    private void Update()
    {
        HandleTileHighlighting();
    }

    // Обработка подсвечивания плиток.
    private void HandleTileHighlighting()
    {
        // Создаем луч, направленный от позиции мыши.
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        GameObject hitObject = null; // Переменная для хранения ссылки плитки, на которую указывает мышь.

        // Проверяем, есть ли столкновение луча с объектом, который находится на слое _highlightLayer.
        if (Physics.Raycast(ray, out var hitInfo, 50f, _highlightLayer))
        {
            hitObject = hitInfo.collider.gameObject; // Получаем объект, с которым столкнулся луч.
            HighlightTile(hitObject); // Подсвечиваем этот объект.
        }

        UpdateLastHighlightedTile(hitObject); // Обновляем последнюю подсвеченную плитку.
    }
    
    // Обновление информации о последней подсвеченной плитке.
    private void UpdateLastHighlightedTile(GameObject currentTile)
    {
        if (currentTile != null && currentTile != _lastHighlightedTile)
        {
            UnhighlightLastTile(); // Если есть новая плитка, снимаем подсветку с предыдущей.
            _lastHighlightedTile = currentTile; // Обновляем последнюю подсвеченную плитку.
        }
        else if (currentTile == null && _lastHighlightedTile != null)
        {
            UnhighlightLastTile(); // Если мышь не указывает на плитку, снимаем подсветку с предыдущей.
        }
    }

    // Подсветка плитки.
    private void HighlightTile(GameObject tile)
    {
        tile.GetComponent<Renderer>().material.color = Color.green; // Изменяем цвет материала плитки на зеленый.
    }

    // Снятие подсветки с предыдущей плитки.
    private void UnhighlightLastTile()
    {
        if (_lastHighlightedTile != null)
        {
            Renderer tileRenderer = _lastHighlightedTile.GetComponent<Renderer>();

            // Проверяем, есть ли у плитки компонент Renderer.
            if (tileRenderer != null)
            {
                tileRenderer.material.color = Color.white; // Восстанавливаем цвет материала плитки на белый.
            }
        }

        _lastHighlightedTile = null; // Сбрасываем последнюю подсвеченную плитку.
    }
}

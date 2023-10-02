using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPooler : MonoBehaviour
{
    [SerializeField] private int _poolSize;
    [SerializeField] private int _insufficientPoolSize;
    [SerializeField] private CellContent _cellContentPrefab;

    private Queue<CellContent> _contentQueue = new Queue<CellContent>();

    public void SpawnPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CellContent spawnedContent = Instantiate(_cellContentPrefab);
            spawnedContent.gameObject.SetActive(false);

            _contentQueue.Enqueue(spawnedContent);
        }
    }

    public CellContent GiveCellContent()
    {
        for (int i = 0; i < _contentQueue.Count; i++)
        {
            var content = _contentQueue.Dequeue();
            _contentQueue.Enqueue(content);

            if (content.gameObject.activeInHierarchy.Equals(true))
            {
                continue;
            }

            content.gameObject.SetActive(true);

            content.transform.localScale = Vector3.one;

            return content;
        }

        CellContent newContent = SpawnInsufficientContent(_cellContentPrefab);
        newContent.gameObject.SetActive(true);
        newContent.transform.localScale = Vector3.one;
        return newContent;
    }

    private CellContent SpawnInsufficientContent(CellContent cellContentPrefab)
    {
        CellContent spawnedContent = null;

        for (int i = 0; i < _insufficientPoolSize; i++)
        {
            spawnedContent = Instantiate(_cellContentPrefab);
            spawnedContent.gameObject.SetActive(false);

            _contentQueue.Enqueue(spawnedContent);
        }

        spawnedContent.transform.localScale = Vector3.one;
        return spawnedContent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorPrey;

public class EnvironmentBehaviour : MonoBehaviour {
    // The environment
    private Environment _environment;
    public GameObject AntPrefab;
    public GameObject DoodlebugPrefab;

    // Start is called before the first frame update
    void Start() {
        _environment = new Environment(10, this); // Derived from ActressMas.TurnBasedEnvironment
    }

    // Get game object
    public void LoadGameObject(Cell currentCell) {
        Die(currentCell);
        if(currentCell.GetState() == CellState.Ant) {
            Load(AntPrefab, currentCell);
        } else if(currentCell.GetState() == CellState.Doodlebug) {
            Load(DoodlebugPrefab, currentCell);
        }
    }

    // Get game object
    public void Die(Cell currentCell) {
        if(currentCell.GameObject != null) {
            Destroy(currentCell.GameObject);
        }
    }

    // Load prefab
    private void Load(GameObject prefab, Cell currentCell) {
        Vector2 origin = Tools.GetOriginLocationInContainer(gameObject, prefab);
        currentCell.GameObject = Instantiate(prefab, new Vector3(origin.x + currentCell.Index.Item1, origin.y + currentCell.Index.Item2, 0), Quaternion.identity);
        currentCell.GameObject.transform.parent = gameObject.transform;     
    }

    /// <summary>
    /// Get origin
    /// </summary>
    /// <param name="obj">Game object</param>
    /// <returns>Origin</returns>
    public Vector3 GetOrigin(GameObject obj) {
        return Tools.GetOriginLocationInContainer(gameObject, obj);
    }

    // Update is called once per frame
    void Update() {
        _environment.Update(Time.deltaTime);
    }
}

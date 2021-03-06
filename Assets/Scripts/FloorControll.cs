using System;
using UnityEngine;
using System.Collections;
using MapUtility;

// Floorシーンを制御するクラス
public class FloorControll : MonoBehaviour {

    // ゲーム状態
    private GameStatus gameStatus;

    // マップ
    private GameObject map;

    // ゲームオブジェクトを初期化する
    void Start() {

        // ゲームオブジェクトからゲーム状態クラスのインスタンスを取得する
        gameStatus = GameObject.Find("GameStatus").GetComponent<GameStatus>();

        // Mapゲームオブジェクトを取得する
        map = GameObject.Find("Map");

        Debug.Log("FloorStarted. FloorLevel: " + gameStatus.FloorLevel);

        generateMap();
        // TODO 敵ユニットを配置する
        // TODO アイテムを配置する
        // TODO 階段を設置する
    }

    // フレーム毎の更新処理
    void Update() {

        // ゲームオーバーになったらResultシーンへ遷移する
        if (gameStatus.GameOver) {
            Application.LoadLevel("Result");
        }

        // TODO 部屋に入った時の判定、処理

    }

    // マップ生成
    private void generateMap() {

        // 全マップチップのプレハブを読み込む
        var wallPrefab = (GameObject)Resources.Load("Prefabs/MapChip/Wall");
        var roadPrefab = (GameObject)Resources.Load("Prefabs/MapChip/Road");
        var roomPrefab = (GameObject)Resources.Load("Prefabs/MapChip/Room");
        var gatePrefab = (GameObject)Resources.Load("Prefabs/MapChip/Gate");

        // マップチップの幅を取得
        var chipSize = wallPrefab.GetComponent<SpriteRenderer>().bounds.size.x * 0.8f;

        // ランダムマップパターンを生成
        var mapArray = MapUtility.MapGenerator.Generate();

        // 対応するプレハブからマップチップのゲームオブジェクトを生成
        for (int x = 0; x < mapArray.GetLength(0); x++) {
            for (int y = 0; y < mapArray.GetLength(1); y++) {
                GameObject prefab = null;
                switch (mapArray[x, y]) {
                    case MapChip.Wall:
                        prefab = wallPrefab;
                        break;
                    case MapChip.Road:
                        prefab = roadPrefab;
                        break;
                    case MapChip.Room:
                        prefab = roomPrefab;
                        break;
                    case MapChip.Gate:
                        prefab = gatePrefab;
                        break;
                }
                if (prefab != null) {
                    GameObject mapChipObject = (GameObject)Instantiate(prefab, new Vector3(chipSize * x, chipSize * y, 0), Quaternion.identity);
                    mapChipObject.transform.parent = map.transform;
                }
            }
        }
    }


    // ここから下はデバッグ用

    public void GameOverButton() {
        gameStatus.GameOver = true;
    }

    public void NextFloorButton() {
        gameStatus.FloorLevel++;

        Application.LoadLevel("Floor");
    }
}

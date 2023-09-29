# LiDARデータの可視化

## 概要

ロボットに搭載されているLiDARデータの可視化方法を紹介します。
今回もオドメトリ可視化の際と同様にLiDARデータ受信用スクリプトとLiDARデータ描画用スクリプトを組み合わせて可視化します。

本ステップ実行後の状態のSceneファイルは[`MobileRobotUITutorialProject/Assets/Scenes/Step5.unity`](../MobileRobotUITutorialProject/Assets/Scenes/Step5.unity)から入手できます。

## 動作確認済環境

* Windows
  * Windows 10 Home バージョン 21H2
  * Windows 11 Pro バージョン 22H2
* Unity
  * Unity 2021.3.4f1
  * Unity 2022.3.8f1
* [Unity-Technologies/ROS-TCP-Connector](https://github.com/Unity-Technologies/ROS-TCP-Connector) v0.7.0
* ROS 2
  * ROS 2 Foxy Fitzroy
  * ROS 2 Humble Hawksbill

## 手順

### 1. LiDARデータ受信用スクリプトをアタッチ

[STEP4](./step4.md)までと同様に`Assets/Scripts/LaserScanSubscriber.cs`を`Subscriber`オブジェクトにアタッチします。

ここまでで`Subscriber`に追加したコンポーネントは`Tf Subscriber`、`Odom Subscriber`、`Laser Scan Subscriber`の3つになります。

![](./images/step6-1.png)

### 2. LiDARデータ描画用スクリプトをアタッチ

まず、スクリプトを用意します。[`UnityScripts/PointCloud`](../UnityScripts/PointCloud)フォルダをUnityプロジェクトの`Assets`フォルダにコピー（`Assets/PointCloud`として配置）します。

![](./images/step6-2.png)

次に用意したスクリプトをアタッチしていきます。
`Assets/PointCloud/Scripts/PointCloudLocator.cs`を`lidar_link`オブジェクト（`raspimouse/base_footprint/base_link/urg_mount_link/lidar_link`）にアタッチします。

![](./images/step6-3.gif)

`lidar_link`オブジェクトを選択してInspectorウィンドウを開き、`Point Cloud Locator`コンポーネントの`Subscriber Game Object`に`Subsciber`オブジェクトを、`Point Prefab`に`Assets/PointCloud/Prefabs/Sphere.prefab`を指定します。

![](./images/step6-4.gif)

以上で可視化の準備ができました。

### 3. Unityプロジェクトの実行

[STEP2](./step2.md)と同様に、

1. 再生モードでUnityプロジェクトを実行
2. Raspberry Pi Mouseでコマンドを実行
3. 操作ボタンから移動指令送信

の手順でロボットを動かしてみます。

![](./images/step6-5.gif)

[STEP4](./step4.md)で可視化したオドメトリに加えてLiDARデータを可視化することができました。

`Assets/PointCloud/Scripts/PointCloudLocator.cs`の`private static readonly int MaxPointCount = 100;`を書き換えることで
描画する点群の数を変えることができます。

最後に、動作確認が終わったら全ての端末でCtrl+Cを押してコマンドを終了させます。

### 本STEPのまとめ

LiDARデータ受信用スクリプトとLiDARデータ描画用スクリプトを組み合わせて可視化する方法を紹介しました。
さらに描画する点群の数を変更する方法も紹介しました。

次は[STEP6](./step6.md)でカメラ画像の可視化方法を紹介します。

---

* [目次](./intro2.md)
* < [STEP4](./step4.md)
* \> [STEP6](./step6.md)
# オドメトリの可視化

## 概要

ロボットのオドメトリを可視化する方法を紹介します。
今回はオドメトリ受信用スクリプトとオドメトリ描画用スクリプトを組み合わせて可視化します。

本ステップ実行後の状態のSceneファイルは[`MobileRobotUITutorialProject/Assets/Scenes/Step4.unity`](../MobileRobotUITutorialProject/Assets/Scenes/Step4.unity)から入手できます。

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

### 1. オドメトリ受信用スクリプトをアタッチ

[STEP3](./step3.md)までと同様に`Assets/Scripts/OdomSubscriber.cs`を`Subscriber`オブジェクトにアタッチします。

ここまでで`Subscriber`に追加したコンポーネントは`Tf Subscriber`と`Odom Subscriber`の2つになります。

![](./images/step5-1.png)

### 2. オドメトリ描画用スクリプトをアタッチ

まず、スクリプトを用意します。[`UnityScripts/OdometryViewer`](../UnityScripts/OdometryViewer)フォルダをUnityプロジェクトの`Assets`フォルダにコピー（`Assets/OdometyViewer`として配置）します。

![](./images/step5-2.png)

次に用意したスクリプトをアタッチしていきます。
`Assets/OdometyViewer/Scripts/OdometryViewer.cs`を`raspimouse`オブジェクトにアタッチします。

![](./images/step5-3.gif)

`raspimouse`オブジェクトを選択してInspectorウィンドウを開き、`Odometry Viewer`コンポーネントの`Subscriber Game Object`に`Subsciber`オブジェクトを、`Arrow Prefab`に`Assets/OdometryViewer/Prefabs/Arrows-red.prefab`を指定します。

![](./images/step5-4.gif)

以上で可視化の準備ができました。

### 3. Unityプロジェクトの実行

[STEP2](./step2.md)と同様に、

1. 再生モードでUnityプロジェクトを実行
2. Raspberry Pi Mouseでコマンドを実行
3. 操作ボタンから移動指令送信

の手順でロボットを動かしてみます。

![](./images/step5-5.gif)

[STEP3](./step3.md)では実機の移動ロボットに合わせてUnityのロボットも動かすことができましたが、
今回はそれに加えてオドメトリを可視化することができました。

`Odometry Viewer`コンポーネントの`Length Of History`のパラメータを変えることで描画するオドメトリの履歴数を変更できます。

![](./images/step5-6.png)

![](./images/step5-7.png)

最後に、動作確認が終わったら全ての端末でCtrl+Cを押してコマンドを終了させます。

## 本STEPのまとめ

オドメトリ受信用スクリプトとオドメトリ描画用スクリプトを組み合わせて可視化する方法を紹介しました。
さらにパラメータを変更して描画する履歴の数を変更する方法も紹介しました。

次は[STEP5](./step5.md)でLiDARデータの可視化方法を紹介します。

---

* [目次](./intro2.md)
* < [STEP3](./step3.md)
* \> [STEP5](./step5.md)
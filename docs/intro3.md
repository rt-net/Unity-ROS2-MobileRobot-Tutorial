# Raspberry Pi Mouseのセットアップ

Raspberry Pi Mouseのセットアップを行います。

[Software Tutorials](https://rt-net.github.io/tutorials/raspimouse/products.html)を参考にして、
ROS 2のサンプルプログラムが動かせる状態のRaspberry Pi Mouseを用意してください。

動作確認済みのROS 2のバージョンは以下の通りです。

* ROS 2 Foxy Fitzroy
* ROS 2 Humble Hawksbill

まず、Raspberry Pi Mouseの電源を入れます。
その後、`Tera Term`のような端末エミュレータを利用するなどして、
PCからRaspberry Pi MouseへSSHで接続します。
このとき、PCとRaspberry Pi Mouseが同じネットワークに接続していることを確認してください。

* SSHによる接続方法に関することは、「SSH Windows 使い方」 などのキーワードでネット検索すると多くの情報が得られますので参考にしてみて下さい。

次に、以下のコマンドを実行して、Raspberry Pi Mouseに`rplidar_ros`をインストールします。
`rplidar_ros`はRaspberry Pi Mouseに搭載しているRPLiDARを使用するためにインストールします。

```sh
# パッケージをclone
mkdir -p ~/ros2_ws/src
cd ~/ros2_ws/src
git clone -b ros2 https://github.com/allenh1/rplidar_ros.git

# パッケージをビルド
cd ~/ros2_ws/
colcon build --symlink-install
source ./install/setup.bash
```

さらに、以下のコマンドを実行して、`v4l2_camera`をインストールします。
`v4l2_camera`はRaspberry Pi Mouseに搭載しているウェブカメラを使用するためにインストールします。

```sh
# ROS 2 Foxy Fitzroyの場合
sudo apt install ros-foxy-v4l2-camera
# ROS 2 Humble Hawksbillの場合
sudo apt install ros-humble-v4l2-camera
```

最後に、以下のコマンドを順番に実行して、`ROS-TCP-Endpoint`をインストールします。
`ROS-TCP-Endpoint`はROS 2とUnityの間で通信を行うためにインストールします。

```sh
cd ~/ros2_ws/src/
git clone -b ROS2v0.7.0 https://github.com/Unity-Technologies/ROS-TCP-Endpoint.git

cd ~/ros2_ws
colcon build --symlink-install
```

以上で、Raspberry Pi Mouseで使用するパッケージの準備ができました。

---

* [目次](./intro2.md)
* < [INTRO2](./intro2.md)
* \> [STEP0](./step0.md)
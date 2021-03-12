## 【GDPR対応】ユーザー情報取得・使用許諾ライブラリ

### 導入方法

TTConsent.packageがあるのでご利用ください

#### Prefabを配置

起動時に開かれるSceneのヒエラルキーに以下を追加する
`_TTConsent/Prefab/TTConsent.prefab`

インスペクタで、
- DeveloperName
- Privacy Url（プライバシーポリシー）
- Terms Url（利用規約）
に変更があれば修正する

※プログラムで変更する場合は以下

```
TTConsent.Instance.DeveloperName = "山形通信"
TTConsent.Instance.DeveloperPrivacyUrl = "https://hogehoge.com";
TTConsent.Instance.DeveloperTermsUrl = "https://hogehoge.com";
```

### 実装方法

#### 同意ダイアログの必要有無判定開始（EU圏内かどうかの判定）

`StartCheckNeedConsentAsync（）`を呼び出し、EU圏内かどうか確認を行う
処理は非同期で実行され、同意のダイアログを表示する必要があるかは後述する`NeedShowDialog`を使用する

```
#if UNITY_EDITOR
	TTConsent.Instance.StartCheckNeedConsentAsync(true);
#else
	TTConsent.Instance.StartCheckNeedConsentAsync();
#endif
```

※この処理を待っていると、アプリの起動に時間がかかり継続率に影響してくる可能性があるため、広告読み込みやその他SDKの初期化等、普通にアプリ起動は進めてしまってOK

※EU圏内かどうかの判定ロジックはGoogleが用意しているConsent SDKの機能を利用している
内部的には
https://adservice.google.com/getconfig/pubvendors
上記URLに通信して、EU圏内かどうかをIPで判定している

#### 同意ダイアログ表示

`NeedShowDialog（）`で表示が必要かどうか確認できる
Update等で同意ダイアログを表示するかどうかを監視して
必要があれば`ShowDialog（）`で表示する

```
void Update()
{
	// 同意が必要な場合、ダイアログを表示
	if (TTConsent.Instance.NeedShowDialog())
	{
		TTConsent.Instance.ShowDialog(
      ダイアログを表示する親Tranceform(UGUIで表示されるのでCanvas配下）,
      (bool isConsent) =>
			{
				// isConsentには同意したかどうかが入ります
				// 同意画面は同意ボタンしかないので、ぶっちゃげisConsentは常にtrueです
				
				// バナー広告を表示するなどの同意後の処理を記載
  		}
  	);
  }
}
```

### テスト方法

#### IDFAを利用したテスト

Resourcesフォルダ直下で

｀右クリック -> Create -> TTConsent -> DebugDevices｀
`TTConsentDebugDevices`を作成してください

インスペクタ上で、テストを行いたい端末のIDFAを登録し
`StartCheckNeedConsentAsync()`をデバッグフラグをtrueで実行することでテストできます

```
TTConsent.Instance.StartCheckNeedConsentAsync(true);
```

#### IP偽造を利用したテスト

TunnelBearというサービスを利用してIP偽造を行なってください
一ヶ月500MBの通信料であれば無料でいけます

アカウントを作成して

https://apps.apple.com/jp/app/tunnelbear-vpn-wifi-proxy/id564842283

をインストールして
起動してEU圏内のVPNを経由するように設定すればOKです（スペインとか）
※VPN経由なので通信は遅くなるでご注意ください。

### 注意点

- 同意が完了する　まで広告の表示はしないでください
同意が完了して広告を表示してよいかの判定は`CanShowAd()`で可能です

```
protected void Start()
{
	if (TTConsent.Instance.CanShowAd())
	{
		// バナー表示など
	}
}
```

### その他

- 同意が終わらないとターゲティング広告読み込みも解析もやっちゃいけないんじゃないの？

本来であればNG。
ただ厳密に対応しようとなると、EU圏内確認処理を待つ必要があります。
（GAとか初期化するだけで解析を飛ばしたりするSDKもあるので）

その遅延でアプリのKPIに支障をきたすことと成りかねない
他者も結構ゆるくやってる感じがあるので、そこまで厳密にやらなくてもOKって感じで進めます。

とはいえ、バナー広告はちょっと出てるとおや？ってなるのでお気をつけください。
(インステとか動画リワードに関しては、表示するまでに同意は終わってると仮定しちゃいます）

- 同意画面の文言ってどうなってるの？

「GDPR 同意画面文言」っていうスプレッドシートで管理していて
S3ではなくEC2のインスタンスに文言を配置しております

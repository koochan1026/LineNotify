namespace LineNotify {

    public class LineNotify {

        /* HttpClientクラス */
        private readonly System.Net.Http.HttpClient pvd_http_client = new System.Net.Http.HttpClient();

        /* LINE通知のリクエスト先 */
        private readonly System.String as1_BaseUrl = "https://notify-api.line.me/api/notify";

        /* リクエストデータ */
        private System.Collections.Generic.Dictionary<System.String, System.String> as1_ReqData = new() {
            {"message", System.String.Empty}
        };

        /***************************************************/
        /* 概要  : コンストラクタ                          */
        /***************************************************/
        public LineNotify() { }


        /***************************************************/
        /* 概要  : ライン通知送信                          */
        /***************************************************/
        /* 引数1 : 認証トークン                            */
        /* 引数2 : メッセージ                              */
        /***************************************************/
        public System.Boolean Send(System.String as1_a_BearerToken, System.String as1_a_Message) {

            /* リクエストデータを設定 */
            this.as1_ReqData["message"] = as1_a_Message;
            /* 認証データを設定 */
            System.String as1_t_authData = "Bearer " + as1_a_BearerToken;


            /* POSTリクエストのインスタンス生成 */
            System.Net.Http.HttpRequestMessage pvd_t_ReqInfo = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, this.as1_BaseUrl);

            /* リクエストデータをUTF-8でエンコードする */
            pvd_t_ReqInfo.Content = new System.Net.Http.FormUrlEncodedContent(this.as1_ReqData);
            /* ヘッダ追加 */
            pvd_t_ReqInfo.Headers.Add("Authorization", as1_t_authData);

            /* リクエスト実行 */
            System.Net.Http.HttpResponseMessage pvd_t_responce = this.pvd_http_client.SendAsync(pvd_t_ReqInfo).Result;

            /* 実行結果を返す */
            if (pvd_t_responce.StatusCode != System.Net.HttpStatusCode.OK) { return false; }
            return pvd_t_responce.Content.ReadAsStringAsync().Result.Contains("ok");
        }

    }


}

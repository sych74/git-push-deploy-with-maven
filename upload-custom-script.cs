//@auth
//@required('url', 'scriptName', 'scriptType')

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Collection;
import com.hivext.api.server.development.response.ApplicationInfoResponse;


//reading script from URL
int READ_TIMEOUT = 30 * 1000;
int CONNECT_TIMEOUT = 10 * 1000;
int BUFFER_SIZE = 2048;

String url = getParam("url");

HttpURLConnection conn = null;
InputStream cis = null;
ByteArrayOutputStream out = null;
try {
    conn = (HttpURLConnection) new URL(url).openConnection();
    conn.setInstanceFollowRedirects(false);
    conn.setReadTimeout(CONNECT_TIMEOUT);
    conn.setConnectTimeout(READ_TIMEOUT);

    byte[] buff = new byte[BUFFER_SIZE];
    cis = conn.getInputStream();
    out = new ByteArrayOutputStream();
    int n;
    while ((n = cis.read(buff)) > -1) {
        out.write(buff, 0, n);
    }


} finally {
    if (cis != null) {
        try {
            cis.close();
        } catch (IOException ex) {}
    }
    if (out != null) {
        try {
            out.close();
        } catch (IOException ex) {}
    }

    if (conn != null) {
        conn.disconnect();
    }

}

//getting appid of application for custom scripts  
String targetAppid = null;
Collection <ApplicationInfoResponse> apps = hivext.dev.apps.GetApps().getApps();
for (ApplicationInfoResponse app: apps) {
    if (app.getName() == "Custom Scripting") {
        targetAppid = app.getAppid();
        break;
    }
}

//creating a new script 
return getParam("user") + " : " + getParam("session") + "  : " + getParam("signature");//hivext.dev.scripting.CreateScript(targetAppid, session, getParam("scriptName"), getParam("scriptType"), out.toString("UTF-8"));

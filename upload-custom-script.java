//@auth
//@required('url', 'scriptName', 'scriptType', 'next')

import java.util.HashMap;
import com.hivext.api.core.utils.Transport;
import com.hivext.api.Response;
import com.hivext.api.server.development.response.ApplicationInfoResponse;
import com.hivext.api.utils.Random;


//reading script from URL
String scriptBody = new Transport().get(getParam("url"));
    
//inject token
String token = Random.getPswd(64);

//delete the script if it already exists
hivext.dev.scripting.DeleteScript(getParam("scriptName"));

//creating a new script 
Response createResp = hivext.dev.scripting.CreateScript(getParam("scriptName"), getParam("scriptType"), scriptBody);
if (!createResp.isOK()) return createResp;

//get scripting domain
String domain = hivext.dev.apps.GetApp(getParam("appid")).getHosting().get("domain").toString();

//building the response
HashMap params = new HashMap();
params.put("domain", domain);
params.put("token", token);
params.put("result", 0);    
    
HashMap call = new HashMap();
call.put("params", params);
call.put("procedure", getParam("next"));
    
HashMap afterReturn = new HashMap();
afterReturn.put("call", call);
    
HashMap eventResponse = new HashMap();
eventResponse.put("onAfterReturn", afterReturn);
eventResponse.put("result", 0);    

HashMap resultResp = new HashMap();
resultResp.put("response", eventResponse);
    
return resultResp;

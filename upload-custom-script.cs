//@auth
//@required('url', 'scriptName', 'scriptType', 'next')

import com.hivext.api.core.utils.Transport;
import com.hivext.api.utils.Random;

//reading script from URL
var scriptBody = new Transport().get(url)

//inject token
var token = Random.getPswd(64);

//delete the script if it exists already
jelastic.dev.scripting.DeleteScript(scriptName);

//creating a new script 
var resp = hivext.dev.scripting.CreateScript(scriptName, scriptType, scriptBody);
if (!resp.result) return resp;

//get scripting domain
var domain = jelastic.dev.apps.GetApp(appid).hosting.domain;

return {
    result: 0,
    onAfterReturn : {
        call : {
            procedure : next,
            params : {
                domain : domain,
                token : token
            }
        }
    }
}

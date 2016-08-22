//@auth
//@req(name, url, branch)

var params = {
   name: name,
   envName: "${env.appid}",
   env: "${env.domain}",
   nodeId: "${nodes.build.first.id}",
   session: session,
   type: "git",
   context: "ROOT",
   url: url,
   branch: branch,
   keyId: null,
   login: null,
   password: null,
   autoupdate: true,
   interval: 1,
   autoResolveConflict: true,
   zdt: false
}

//create and update the project 
resp = jelastic.env.vcs.CreateProject(params.envName, params.session, params.nodeId, params.name, params.type, params.url, params.keyId, params.login, params.password, params.env, params.context, params.branch, params.autoupdate, params.interval, params.autoResolveConflict, params.zdt);
if (resp.result != 0) return resp;
resp = jelastic.env.vcs.Update(params.envName, params.session, params.project);
return resp;

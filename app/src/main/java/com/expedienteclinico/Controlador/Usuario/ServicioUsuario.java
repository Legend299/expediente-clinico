package com.expedienteclinico.Controlador.Usuario;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;
import com.expedienteclinico.dto.ExpedienteDTO;
import com.expedienteclinico.dto.UsuarioDTO;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ServicioUsuario {

    Context _context;
    List<UsuarioDTO> _usuario;

    public ServicioUsuario(Context context) {
        _context = context;
        _usuario = new ArrayList<>();
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(List<UsuarioDTO> usuario);
        void onResponse(JSONObject code);
    }

    public void solicitarListaUsuarios(ServicioUsuario.VolleyResponseListener volleyResponseListener) {

        String url = API.QUERY_USUARIO_LISTA.toString();
        StringRequest postRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONArray jsonA = new JSONArray(response);

                    for(int i = 0; i < jsonA.length(); i++)
                    {
                        JSONObject jsonObject = jsonA.getJSONObject(i);
                        UsuarioDTO usuario = new UsuarioDTO();
                        usuario.setIdUsuario(jsonObject.getInt("idUsuario"));
                        usuario.setCorreo(jsonObject.getString("correo"));
                        usuario.setPassword(jsonObject.getString("password"));
                        usuario.setIdRol(jsonObject.getInt("idRol"));
                        usuario.setIdExpediente(jsonObject.getInt("idExpediente"));
                        usuario.setActivo(jsonObject.getBoolean("activo"));
                        _usuario.add(usuario);
                    }
                        volleyResponseListener.onResponse(_usuario);
                    }catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                volleyResponseListener.onError(error.getMessage());
            }
        });
        Volley.newRequestQueue(_context).add(postRequest);
    }

    public void registrarUsuario(String correo, String password, ServicioUsuario.VolleyResponseListener volleyResponseListener){

        String url = API.QUERY_USUARIO_LISTA.toString();

        JSONObject postData = new JSONObject();
        try{

            postData.put("correo", correo);
            postData.put("password", password);
            postData.put("idRol",3);
            postData.put("activo",true);

        }catch(JSONException e){
            e.printStackTrace();
        }

        JsonObjectRequest postRequest = new JsonObjectRequest(Request.Method.POST, url, postData, new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                volleyResponseListener.onResponse(response);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                volleyResponseListener.onError(error.getMessage());
            }
        });
        Volley.newRequestQueue(_context).add(postRequest);
    }
}

package com.expedienteclinico.Controlador.Usuario;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;
import com.expedienteclinico.dto.EstadoDTO;
import com.expedienteclinico.dto.UsuarioDTO;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class ServicioEstado {

    Context _context;
    List<EstadoDTO> _estado;

    public ServicioEstado(Context context) {
        _context = context;
        _estado = new ArrayList<>();
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(List<EstadoDTO> estado);
    }

    public void solicitarListaEstados(ServicioEstado.VolleyResponseListener volleyResponseListener) {

        String url = API.QUERY_ESTADOS_LISTA.toString();
        StringRequest postRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONArray jsonA = new JSONArray(response);
                    for(int i = 0; i < jsonA.length(); i++)
                    {
                        JSONObject jsonObject = jsonA.getJSONObject(i);
                        EstadoDTO estado = new EstadoDTO();

                        estado.setIdEstado(jsonObject.getInt("idEstado"));
                        estado.setNombre(jsonObject.getString("nombre"));

                        _estado.add(estado);
                    }

                    volleyResponseListener.onResponse(_estado);

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
}

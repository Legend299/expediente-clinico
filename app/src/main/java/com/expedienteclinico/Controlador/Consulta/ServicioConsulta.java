package com.expedienteclinico.Controlador.Consulta;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;


public class ServicioConsulta {

    Context _context;

    public ServicioConsulta(Context context){
        _context = context;
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(String response);
    }

    public void SolicitarConsultasUsuario(int idExpediente, VolleyResponseListener volleyResponseListener){

        String url = API.QUERY_CONSULTA_ID_EXPEDIENTE.toString() + idExpediente;
        StringRequest consultaRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                volleyResponseListener.onResponse(response);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                volleyResponseListener.onError(error.getMessage());
            }
        });
        Volley.newRequestQueue(_context).add(consultaRequest);

    }

}

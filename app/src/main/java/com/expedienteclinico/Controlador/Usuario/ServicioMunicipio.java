package com.expedienteclinico.Controlador.Usuario;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;
import com.expedienteclinico.dto.EstadoDTO;
import com.expedienteclinico.dto.MunicipioDTO;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class ServicioMunicipio {
    Context _context;
    List<MunicipioDTO> _municipio;

    public ServicioMunicipio(Context context) {
        _context = context;
        _municipio = new ArrayList<>();
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(List<MunicipioDTO> municipio);
    }

    public void solicitarListaMunicipios(int IdEstado, ServicioMunicipio.VolleyResponseListener volleyResponseListener) {

        String url = API.QUREY_MUNICIPIOS_LISTA_ID_ESTADO.toString()+String.valueOf(IdEstado);
        StringRequest postRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                    JSONArray jsonA = new JSONArray(response);
                    for(int i = 0; i < jsonA.length(); i++)
                    {
                        JSONObject jsonObject = jsonA.getJSONObject(i);
                        MunicipioDTO municipio = new MunicipioDTO();

                        municipio.setIdMunicipio(jsonObject.getInt("idMunicipio"));
                        municipio.setNombre(jsonObject.getString("nombre"));

                        _municipio.add(municipio);
                    }

                    volleyResponseListener.onResponse(_municipio);

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

package com.expedienteclinico.Controlador.Consulta;

import android.content.Context;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;
import com.expedienteclinico.dto.ConsultaDTO;
import com.expedienteclinico.dto.UsuarioDTO;
import com.expedienteclinico.utilidades.ConvertirFecha;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;


public class ServicioConsulta {

    Context _context;
    List<ConsultaDTO> _listaConsulta;
    ConvertirFecha _convertirFecha;

    public ServicioConsulta(Context context){
        _context = context;
        _listaConsulta = new ArrayList<>();
        _convertirFecha = new ConvertirFecha();
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(List<ConsultaDTO> response);
    }

    public void SolicitarConsultasUsuario(int idExpediente, VolleyResponseListener volleyResponseListener){

        String url = API.QUERY_CONSULTA_ID_EXPEDIENTE.toString() + idExpediente;
        StringRequest consultaRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {

                try {
                    JSONArray jsonA = new JSONArray(response);

                    for(int i = 0; i < jsonA.length(); i++)
                    {
                        JSONObject jsonObject = jsonA.getJSONObject(i);
                        ConsultaDTO consulta = new ConsultaDTO();

                        consulta.setIdConsulta(jsonObject.getInt("idConsulta"));
                        consulta.setFecha(_convertirFecha.recibirFecha(jsonObject.getString("fecha")));
                        consulta.setMedico(jsonObject.getString("medico"));
                        consulta.setIdExpediente(jsonObject.getInt("idExpediente"));
                        consulta.setDiagnostico(jsonObject.getString("diagnostico"));

                        _listaConsulta.add(consulta);
                    }
                    volleyResponseListener.onResponse(_listaConsulta);
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
        Volley.newRequestQueue(_context).add(consultaRequest);

    }

}

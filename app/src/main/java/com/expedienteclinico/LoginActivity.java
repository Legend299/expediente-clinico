package com.expedienteclinico;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.Controlador.Expediente.ServicioExpediente;
import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.Controlador.Usuario.ServicioUsuario;
import com.expedienteclinico.dto.ExpedienteDTO;
import com.expedienteclinico.dto.UsuarioDTO;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

public class LoginActivity extends AppCompatActivity {

    private EditText txtCorreo,txtPassword;
    private Button btnLogin,btnRegistrarse;
    private ProgressBar carga;

    private LinearLayout LoginLayout;
    private AnimationDrawable animationDrawable;

    private List<UsuarioDTO> lista;

    private SessionUsuario sessionUsuario = SessionUsuario.getInstance();

    //static SessionUsuario sessionUsuario;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        LoginLayout = findViewById(R.id.loginLayout);
        txtCorreo = findViewById(R.id.txtCorreo);
        txtPassword = findViewById(R.id.txtPassword);
        btnLogin = findViewById(R.id.btnLogin);
        btnRegistrarse = findViewById(R.id.btnRegistrarse);
        carga = findViewById(R.id.idLoadingPB);

        animationDrawable = (AnimationDrawable) LoginLayout.getBackground();
        animationDrawable.setEnterFadeDuration(4500);
        animationDrawable.setExitFadeDuration(4500);
        animationDrawable.start();

        btnRegistrarse.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(LoginActivity.this, SignUpActivity.class);
                startActivity(intent);
            }
        });

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                    carga.setVisibility(View.VISIBLE);
                    ServicioUsuario servicioUsuario = new ServicioUsuario(LoginActivity.this);
                    servicioUsuario.solicitarListaUsuarios(new ServicioUsuario.VolleyResponseListener() {
                        @Override
                        public void onError(String message) {
                            Toast.makeText(LoginActivity.this,"No se ha podido realizar conexión al servidor.", Toast.LENGTH_LONG).show();
                            carga.setVisibility(View.GONE);
                        }

                        @Override
                        public void onResponse(List<UsuarioDTO> usuario) {

                            lista = usuario;
                            String correo = txtCorreo.getText().toString();
                            String password = txtPassword.getText().toString();
                            if(ValidarUsuario(correo,password))
                            {
                                System.out.println("CUENTA: ");
                                Log.e("ID USUARIO ",String.valueOf(sessionUsuario.getIdUsuario()));
                                Log.e("CORREO: ",sessionUsuario.getCorreo());
                                Log.e("PASSWORD: ",sessionUsuario.getPassword());
                                Log.e("ID ROL: ",String.valueOf(sessionUsuario.getIdRol()));
                                Log.e("ID EXPEDIENTE: ",String.valueOf(sessionUsuario.getIdExpediente()));
                                Log.e("ACTIVO: ",String.valueOf(sessionUsuario.isActivo()));

                                Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                                startActivity(intent);
                                finish();
                                //Toast.makeText(LoginActivity.this,"Acceso permitido", Toast.LENGTH_LONG).show();
                            } else {
                                Toast.makeText(LoginActivity.this,"Datos de sesión incorrectos", Toast.LENGTH_LONG).show();
                            }
                        }

                        @Override
                        public void onResponse(JSONObject code){
                        }
                    });

            }
        });

    }


    private boolean ValidarUsuario(String correo, String password){
        List<UsuarioDTO> listaUsuarios = lista;
        try {
            for (UsuarioDTO u : listaUsuarios) {
                if (u.getCorreo().equals(correo) && u.getPassword().equals(password)) {

                    //carga.setVisibility(View.GONE);
                    sessionUsuario.setIdUsuario(u.getIdUsuario());
                    sessionUsuario.setCorreo(u.getCorreo());
                    sessionUsuario.setPassword(u.getPassword());
                    sessionUsuario.setIdRol(u.getIdRol());
                    sessionUsuario.setIdExpediente(u.getIdExpediente());
                    sessionUsuario.setActivo(u.isActivo());

                    return true;
                }
            }
        }catch(Exception ex){
            Log.e("ERROR: ",ex.getMessage());
            carga.setVisibility(View.GONE);
        }
        carga.setVisibility(View.GONE);
        return false;
    }


}
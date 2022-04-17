package com.expedienteclinico;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.expedienteclinico.Controlador.Usuario.ServicioUsuario;
import com.expedienteclinico.dto.UsuarioDTO;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class SignUpActivity extends AppCompatActivity {

    private Button btnLogin,btnRegistrarse;
    private EditText txtCorreo,txtPassword1,txtPassword2;
    private ProgressBar carga;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);

        btnLogin = findViewById(R.id.btnLogin);
        btnRegistrarse = findViewById(R.id.btnRegistrarse);

        txtCorreo = findViewById(R.id.txtCorreo);
        txtPassword1 = findViewById(R.id.txtPassword1);
        txtPassword2 = findViewById(R.id.txtPassword2);

        carga = findViewById(R.id.idLoadingPB);

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(SignUpActivity.this, LoginActivity.class);
                startActivity(intent);
                finish();
            }
        });

        btnRegistrarse.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(txtPassword1.getText().toString().equals(txtPassword2.getText().toString())){
                    carga.setVisibility(View.VISIBLE);
                    ServicioUsuario servicioUsuario = new ServicioUsuario(SignUpActivity.this);
                    servicioUsuario.registrarUsuario(txtCorreo.getText().toString(), txtPassword1.getText().toString(), new ServicioUsuario.VolleyResponseListener() {
                        @Override
                        public void onError(String message) {
                            carga.setVisibility(View.GONE);
                            Toast.makeText(SignUpActivity.this,"ERROR #11111: "+message, Toast.LENGTH_LONG).show();
                        }

                        @Override
                        public void onResponse(List<UsuarioDTO> usuario) {

                        }

                        @Override
                        public void onResponse(JSONObject code) {
                            carga.setVisibility(View.GONE);
                            try {
                                Toast.makeText(SignUpActivity.this, "ID USUARIO: " + code.getString("idUsuario"), Toast.LENGTH_LONG).show();
                            }catch(JSONException e){
                                e.printStackTrace();
                            }
                            Intent intent = new Intent(SignUpActivity.this, LoginActivity.class);
                            startActivity(intent);
                            finish();
                        }
                    });

                } else {
                    Toast.makeText(SignUpActivity.this,"Las contraseñas no son iguales", Toast.LENGTH_LONG).show();
                }
            }
        });


    }
}
package com.expedienteclinico;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.Fragment;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.databinding.ActivityMainBinding;
import com.expedienteclinico.ui.consulta.ConsultaAgregarFragment;
import com.expedienteclinico.ui.consulta.ConsultaFragment;
import com.expedienteclinico.ui.expediente.ExpedienteFragment;
import com.google.android.material.navigation.NavigationView;
import com.google.android.material.snackbar.Snackbar;

public class MainActivity extends AppCompatActivity{

    private AppBarConfiguration mAppBarConfiguration;
    private ActivityMainBinding binding;
    private int contarVeces = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());


        setSupportActionBar(binding.appBarMain.toolbar);
        binding.appBarMain.toolbar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "ALGUNA FUNCION EN UN FUTURO", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });

        DrawerLayout drawer = binding.drawerLayout;
        NavigationView navigationView = binding.navView;


        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_inicio, R.id.nav_perfil, R.id.nav_expediente, R.id.nav_consultas, R.id.nav_expediente_editar, R.id.nav_cerrarSesion)
                .setOpenableLayout(drawer)
                .build();



        //ESto
        //LinearLayout linearLayout = (LinearLayout) findViewById(R.id.nav_header_main);
        //TextView textview = (TextView) linearLayout.findViewById(R.id.txtCorreoDisplay);
        //textview.setText(SessionUsuario.getInstance().getCorreo());

        //funciona duplica
        View linearLayout = navigationView.inflateHeaderView(R.layout.nav_header_main);
        TextView txtCorreo = linearLayout.findViewById(R.id.txtCorreoDisplay);
        txtCorreo.setText(SessionUsuario.getInstance().getCorreo());

        TextView txtExpediente = linearLayout.findViewById(R.id.txtExpedienteDisplay);
        if(SessionUsuario.getInstance().getIdExpediente() > 0){
            txtExpediente.setText("ID EXPEDIENTE: "+SessionUsuario.getInstance().getIdExpediente());
        } else{
            txtExpediente.setText("SIN EXPEDIENTE");
        }



        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_main);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        NavigationUI.setupWithNavController(navigationView, navController);

        //navigationView.bringToFront();
        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                switch(item.getItemId()){
                    case R.id.nav_cerrarSesion:
                        Intent i = new Intent(MainActivity.this, LoginActivity.class);
                        startActivity(i);
                        finish();
                        break;
                    case R.id.nav_expediente:
                        getSupportActionBar().setTitle("Expediente");
                        getSupportFragmentManager().beginTransaction().replace(R.id.nav_host_fragment_content_main, new ExpedienteFragment()).addToBackStack(null).commit();
                        drawer.closeDrawers();
                        break;
                    case R.id.nav_consultas:
                        getSupportActionBar().setTitle("Consultas");
                        getSupportFragmentManager().beginTransaction().replace(R.id.nav_host_fragment_content_main, new ConsultaFragment()).addToBackStack(null).commit();
                        drawer.closeDrawers();
                        break;
                    case R.id.nav_perfil:
                        getSupportActionBar().setTitle("Perfil");
                        break;
                    case R.id.nav_documentos:
                        getSupportActionBar().setTitle("Documentos");
                        break;
                }
                return true;
            }
        });

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment_content_main);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }

    @Override
    public void onBackPressed(){
        contarVeces++;
        if(contarVeces >= 10) {
            Toast.makeText(MainActivity.this, "Algo", Toast.LENGTH_LONG).show();
            contarVeces = 0;
        }
    }
}
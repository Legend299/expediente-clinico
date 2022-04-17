package com.expedienteclinico;

import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.databinding.ActivityMainBinding;
import com.google.android.material.navigation.NavigationView;
import com.google.android.material.snackbar.Snackbar;

public class MainActivity extends AppCompatActivity {

    private AppBarConfiguration mAppBarConfiguration;
    private ActivityMainBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());


        setSupportActionBar(binding.appBarMain.toolbar);
        binding.appBarMain.toolbar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });
        DrawerLayout drawer = binding.drawerLayout;
        NavigationView navigationView = binding.navView;
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_inicio, R.id.nav_perfil, R.id.nav_expediente, R.id.nav_consultas, R.id.nav_expediente_editar)
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
}
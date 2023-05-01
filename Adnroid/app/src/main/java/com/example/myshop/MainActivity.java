package com.example.myshop;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.widget.ImageView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.example.myshop.application.HomeApplication;
import com.example.myshop.category.CategoriesAdapter;
import com.example.myshop.constants.Urls;
import com.example.myshop.dto.category.CategoryItemDTO;
import com.example.myshop.service.CategoryNetwork;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends BaseActivity {

    CategoriesAdapter adapter;
    RecyclerView rc;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ImageView avatar = (ImageView)findViewById(R.id.myImage);
        String url = Urls.BASE+"/images/1.jpg";
        Glide.with(HomeApplication.getAppContext())
                .load(url)
                .apply(new RequestOptions().override(600))
                .into(avatar);

        rc = findViewById(R.id.rcvCategories);
        rc.setHasFixedSize(true);
        rc.setLayoutManager(new GridLayoutManager(this, 2, RecyclerView.VERTICAL, false));
        rc.setAdapter(new CategoriesAdapter(new ArrayList<>()));

        requestServer();
    }

    void requestServer() {
        CategoryNetwork
                .getInstance()
                .getJsonApi()
                .list()
                .enqueue(new Callback<List<CategoryItemDTO>>() {
                    @Override
                    public void onResponse(Call<List<CategoryItemDTO>> call, Response<List<CategoryItemDTO>> response) {
                        List<CategoryItemDTO> list = response.body();
                        adapter = new CategoriesAdapter(list);
                        rc.setAdapter(adapter);
                    }

                    @Override
                    public void onFailure(Call<List<CategoryItemDTO>> call, Throwable t) {

                    }
                });
    }
}
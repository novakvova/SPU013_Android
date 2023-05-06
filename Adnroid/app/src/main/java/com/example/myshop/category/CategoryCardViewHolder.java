package com.example.myshop.category;

import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.myshop.R;

public class CategoryCardViewHolder extends RecyclerView.ViewHolder {
    private ImageView categoryImage;
    private TextView categoryName;
    private Button btnDelete;


    public CategoryCardViewHolder(@NonNull View itemView) {
        super(itemView);
        categoryName=itemView.findViewById(R.id.categoryName);
        categoryImage=itemView.findViewById(R.id.categoryImage);
        btnDelete=itemView.findViewById(R.id.btnDelete);
    }

    public ImageView getCategoryImage() {
        return categoryImage;
    }

    public TextView getCategoryName() {
        return categoryName;
    }

    public Button getBtnDelete() {
        return btnDelete;
    }
}

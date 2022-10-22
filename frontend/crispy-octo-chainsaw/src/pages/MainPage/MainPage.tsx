import { Layout } from "antd";
import React from "react";
import { CourseCard } from "../../components/CourseCard";
import PageWrapper from "../../components/PageWrapper";

const courses = [
  {
    image:
      "https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png",
    title: "Card title",
    description: "This is the description",
  },
  {
    image:
      "https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png",
    title: "Card title2",
    description: "This is the description2",
  },
  {
    image:
      "https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png",
    title: "Card title2",
    description: "This is the description2",
  },
  {
    image:
      "https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png",
    title: "Card title2",
    description: "This is the description2",
  },
];

function getCourses() {
  return courses.map((course) => 
    <CourseCard
      image= {course.image}
      title= {course.title}
      description= {course.description}
    />
  );
}

function MainPage() {
  return (
    <>
      <PageWrapper>
        <div className="course-group">
          <>
          {getCourses()}
          </>
        </div>
      </PageWrapper>
    </>
  );
}

export default MainPage;

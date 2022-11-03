const MainPageImage = ({ source, index }) => {
  return <img src={source} alt={index + "- " + source} />;
};

export default MainPageImage;
